using AutoMapper;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Services;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DaiPhatDat.WebHost.Navigation.Application
{
    public class MenuService : BaseService, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly INavNodeRepository _navNodeRepository;
        public MenuService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper
            , IMenuRepository menuRepository,
            INavNodeRepository navNodeRepository) : base(dbContextScopeFactory, mapper)
        {
            _menuRepository = menuRepository;
            _navNodeRepository = navNodeRepository;
        }
        public IQueryable<Menu> GetAll()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _menuRepository.GetAll();
            }
        }
        public async Task<Tuple<int, IReadOnlyList<MenuPagingDto>>> GetPagingAsync(SearchMenuDto dto)
        {
            try
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    var iquery = _menuRepository.GetAll()
                        .Where(x => x.ActiveFag != CommonUtility.ActiveFag.Delete)
                         .GroupJoin(_navNodeRepository.GetAll()
                        .Where(x => x.ActiveFag == CommonUtility.ActiveFag.Active
                        && x.Status != CommonValues.NavNode.NavNodeStatus.Ignore),
                        mn => mn.NavNodeId,
                        nav => nav.Id,
                        (mn, nav) => new { mn, nav = nav.DefaultIfEmpty() })
                         .SelectMany(z => z.nav.Select(nv => new { z.mn, nv }))
                        .Select(x => new
                        {
                            x.mn,
                            NavName = x.nv.Name,
                            x.nv.Areas,
                            x.nv.Controller,
                            x.nv.Action,
                            x.nv.Params,
                            NavURL = x.nv.URL,
                        });
                    if (!string.IsNullOrEmpty(dto.KeyWord))
                    {
                        var trimKeyword = dto.KeyWord.Trim();
                        var urlKeyWord = CommonUtility.ToUnsignString(trimKeyword);
                        iquery = iquery.Where(x => x.mn.Name.ToLower().Contains(trimKeyword)
                        || x.mn.Name.ToLower().Contains(trimKeyword)
                         || x.Areas.ToLower().Contains(trimKeyword)
                        || x.Controller.ToLower().Contains(trimKeyword)
                         || x.Action.ToLower().Contains(trimKeyword)
                        || x.NavURL.Contains(urlKeyWord)
                        || x.mn.URL.Contains(urlKeyWord));
                    }
                    var total = iquery.Count();
                    var data = await iquery
                       .OrderBy(x => x.mn.Code)
                       .ThenBy(x => x.mn.Order)
                       .Skip((dto.PageIndex - 1) * dto.PageSize)
                       .Take(dto.PageSize)
                       .Select(x => new MenuPagingDto
                       {
                           Id = x.mn.Id,
                           Code = x.mn.Code,
                           ParentId = x.mn.ParentId,
                           NavNodeId = x.mn.NavNodeId,
                           Layout = x.mn.Layout,
                           TypeModule = x.mn.TypeModule,
                           Status = x.mn.Status,
                           Name = x.mn.Name,
                           Icon = x.mn.Icon,
                           Areas = x.Areas,
                           Controller = x.Controller,
                           Action = x.Action,
                           Params = x.Params
                       })
                       .ToListAsync();
                    return new Tuple<int, IReadOnlyList<MenuPagingDto>>(total, data);
                }
            }
            catch (Exception ex) { }
            return new Tuple<int, IReadOnlyList<MenuPagingDto>>(0, new List<MenuPagingDto>());
        }


        public List<MenuPagingDto> GetTreeMenu(SurePortalModules module, string code)
        {
            try
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    var iquery = _menuRepository.GetAll()
                        .Where(x => x.ActiveFag != CommonUtility.ActiveFag.Delete)
                         .GroupJoin(_navNodeRepository.GetAll()
                        .Where(x => x.ActiveFag == CommonUtility.ActiveFag.Active
                        && x.Status != CommonValues.NavNode.NavNodeStatus.Ignore),
                        mn => mn.NavNodeId,
                        nav => nav.Id,
                        (mn, nav) => new { mn, nav = nav.DefaultIfEmpty() })
                         .SelectMany(z => z.nav.Select(nv => new { z.mn, nv }))
                        .Select(x => new
                        {
                            x.mn,
                            NavName = x.nv.Name,
                            x.nv.Areas,
                            x.nv.Controller,
                            x.nv.Action,
                            x.nv.Params,
                            NavURL = x.nv.URL,
                        });
                    iquery = iquery.Where(x => x.mn.Code.ToLower().Contains(code) && x.mn.TypeModule == module);
                    var total = iquery.Count();
                    var data = iquery
                       .OrderBy(x => x.mn.Code)
                       .ThenBy(x => x.mn.Order)
                       .Select(x => new MenuPagingDto
                       {
                           Id = x.mn.Id,
                           Code = x.mn.Code,
                           ParentId = x.mn.ParentId,
                           NavNodeId = x.mn.NavNodeId,
                           Layout = x.mn.Layout,
                           TypeModule = x.mn.TypeModule,
                           Status = x.mn.Status,
                           Name = x.mn.Name,
                           Icon = x.mn.Icon,
                           Areas = x.Areas,
                           Controller = x.Controller,
                           Action = x.Action,
                           Params = x.Params
                       })
                       .ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return new List<MenuPagingDto>();
        }

        public List<MenuPagingDto> GetTreeMenu(string code)
        {
            try
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    var iquery = _menuRepository.GetAll()
                        .Where(x => x.ActiveFag != CommonUtility.ActiveFag.Delete)
                         .GroupJoin(_navNodeRepository.GetAll()
                        .Where(x => x.ActiveFag == CommonUtility.ActiveFag.Active
                        && x.Status != CommonValues.NavNode.NavNodeStatus.Ignore),
                        mn => mn.NavNodeId,
                        nav => nav.Id,
                        (mn, nav) => new { mn, nav = nav.DefaultIfEmpty() })
                         .SelectMany(z => z.nav.Select(nv => new { z.mn, nv }))
                        .Select(x => new
                        {
                            x.mn,
                            NavName = x.nv.Name,
                            x.nv.Areas,
                            x.nv.Controller,
                            x.nv.Action,
                            x.nv.Params,
                            NavURL = x.nv.URL,
                        });
                    iquery = iquery.Where(x => x.mn.Code.ToLower().Contains(code));
                    var total = iquery.Count();
                    var data = iquery
                       .OrderBy(x => x.mn.Code)
                       .ThenBy(x => x.mn.Order)
                       .Select(x => new MenuPagingDto
                       {
                           Id = x.mn.Id,
                           Code = x.mn.Code,
                           ParentId = x.mn.ParentId,
                           NavNodeId = x.mn.NavNodeId,
                           Layout = x.mn.Layout,
                           TypeModule = x.mn.TypeModule,
                           Status = x.mn.Status,
                           Name = x.mn.Name,
                           Icon = x.mn.Icon,
                           Areas = x.Areas,
                           Controller = x.Controller,
                           Action = x.Action,
                           Params = x.Params
                       })
                       .ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return new List<MenuPagingDto>();
        }

        public async Task<MenuDto> GetByIdAsync(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var iquery = await _menuRepository.GetAll()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
                if (iquery != null)
                {
                    return _mapper.Map<MenuDto>(iquery);
                }
            }
            return null;
        }

        public async Task<MenuDto> AddAsync(MenuDto dto)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var entity = Menu.Create(dto);
                    _menuRepository.Add(entity);
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<MenuDto>(entity);
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var entity = await _menuRepository.FindAsync(p => p.Id == dto.Id);
                    if (entity != null)
                    {
                        entity.Update(dto);
                        _menuRepository.Modify(entity);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var entity = await _menuRepository.FindAsync(p => p.Id == id);
                    _menuRepository.Delete(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public List<MenuTreeDto> GetAllRecursive()
        {
            try
            {
                #region Sql
                var sqlData = @"WITH MenuTree (Id,Name, [Level])
                                AS (SELECT  Id,
			                                CAST(Name AS NVARCHAR(MAX)),          
                                            0 AS [Level]                                              
                                    FROM nav.Menu   where ParentId is null
                                    UNION ALL
                                    SELECT child.Id,
			                                CAST(CONCAT(REPLICATE('|__', parent.[Level]), '|__', child.Name) AS NVARCHAR(MAX)),           
                                            parent.Level + 1                                                   
                                    FROM nav.Menu child
                                    INNER JOIN MenuTree parent
                                            ON child.ParentId = parent.Id)
                                SELECT *
                                FROM MenuTree
                                ORDER BY [Level];";
                #endregion
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    return _menuRepository.SqlQuery<MenuTreeDto>(sqlData).ToList();
                }
            }
            catch (Exception ex) { }
            return new List<MenuTreeDto>();
        }

        public List<DaiPhatDat.Core.Kernel.Orgs.Domain.Role> GetAllRole()
        {
            try
            {
                #region Sql
                var sqlData = @"SELECT[ID]
                                  ,[Name]
                                  ,[Description]
                                  ,[ModuleId]
                              FROM[SurePortal].[dbo].[Roles]";
                #endregion
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    return _menuRepository.SqlQuery<DaiPhatDat.Core.Kernel.Orgs.Domain.Role>(sqlData).ToList();
                }
            }
            catch (Exception ex) { }
            return new List<DaiPhatDat.Core.Kernel.Orgs.Domain.Role>();
        }
    }
}