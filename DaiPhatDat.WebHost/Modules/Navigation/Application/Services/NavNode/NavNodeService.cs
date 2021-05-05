using AutoMapper;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
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
    public class NavNodeService : BaseService, INavNodeService
    {
        private readonly INavNodeRepository _navNodeRepository;

        public NavNodeService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper
            , INavNodeRepository navNodeRepository) : base(dbContextScopeFactory, mapper)
        {
            _navNodeRepository = navNodeRepository;
        }
        public IQueryable<NavNode> GetAll()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _navNodeRepository.GetAll();
            }
        }
        public async Task<Tuple<int, IReadOnlyList<NavNodePagingDto>>> GetPagingAsync(SearchMenuDto dto)
        {
            try
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    var iquery = _navNodeRepository.GetAll()
                        .Where(x => (x.ActiveFag != CommonUtility.ActiveFag.Delete
                            && x.Status != CommonValues.NavNode.NavNodeStatus.Ignore)
                        || (x.Status == CommonValues.NavNode.NavNodeStatus.Ignore
                            && string.IsNullOrEmpty(x.ModifiedBy)));
                    if (!string.IsNullOrEmpty(dto.KeyWord))
                    {
                        var trimKeyword = dto.KeyWord.Trim();
                        var urlKeyWord = CommonUtility.ToUnsignString(trimKeyword);
                        iquery = iquery.Where(x => x.Name.ToLower().Contains(trimKeyword)
                        || x.Name.ToLower().Contains(trimKeyword)
                         || x.Areas.ToLower().Contains(trimKeyword)
                        || x.Controller.ToLower().Contains(trimKeyword)
                         || x.Action.ToLower().Contains(trimKeyword)
                        || x.URL.Contains(urlKeyWord));
                    }
                    var total = iquery.Count();
                    var data = await iquery
                       .OrderBy(x => x.Name)
                       .Skip((dto.PageIndex - 1) * dto.PageSize)
                       .Take(dto.PageSize)
                       .Select(x => new NavNodePagingDto
                       {
                           Id = x.Id,
                           Status = x.Status,
                           Name = x.Name,
                           Areas = x.Areas,
                           Controller = x.Controller,
                           Action = x.Action,
                           Params = x.Params
                       })
                       .ToListAsync();
                    return new Tuple<int, IReadOnlyList<NavNodePagingDto>>(total, data);
                }
            }
            catch (Exception ex) { }
            return new Tuple<int, IReadOnlyList<NavNodePagingDto>>(0, new List<NavNodePagingDto>());
        }
        public async Task<List<NavNodeDto>> EasyAutocomplete(string keyword)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var iquery = _navNodeRepository.GetAll()
                    .Where(x => x.Status != CommonValues.NavNode.NavNodeStatus.Ignore);
                if (!string.IsNullOrEmpty(keyword))
                {
                    var trimKeyword = keyword.Trim();
                    var urlKeyWord = CommonUtility.ToUnsignString(trimKeyword);
                    iquery = iquery.Where(x => x.Name.ToLower().Contains(trimKeyword)
                    || x.Name.ToLower().Contains(trimKeyword)
                     || x.Areas.ToLower().Contains(trimKeyword)
                    || x.Controller.ToLower().Contains(trimKeyword)
                     || x.Action.ToLower().Contains(trimKeyword)
                    || x.URL.Contains(urlKeyWord));
                }
                var data = await iquery
                    .OrderBy(x => x.Areas)
                    .ThenBy(x => x.Controller)
                    .ThenBy(x => x.Action)
                    .Take(10)
                    .ToListAsync();
                return _mapper.Map<List<NavNodeDto>>(data);
            }
        }
        public async Task<IReadOnlyList<NavNodeDto>> GetList()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _navNodeRepository
                     .Query<NavNodeDto>()
                     .OrderBy(x => x.Name)
                     .ToListAsync();
            }
        }
        public async Task<NavNodeDto> GetByIdAsync(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var iquery = await _navNodeRepository.GetAll()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
                if (iquery != null)
                {
                    return _mapper.Map<NavNodeDto>(iquery);
                }
            }
            return null;
        }
        public async Task<bool> AddAsync(NavNodeDto dto)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var entity = NavNode.Create(dto);
                    _navNodeRepository.Add(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public async Task<bool> AddRangeAsync(List<NavNodeDto> lstDto)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var LstEntity = new List<NavNode>();
                    foreach (var item in lstDto)
                    {
                        LstEntity.Add(NavNode.Create(item));
                    }
                    _navNodeRepository.AddRange(LstEntity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public async Task<bool> UpdateAsync(NavNodeDto dto)
        {
            try
            {
                using (var dbContext = _dbContextScopeFactory.Create())
                {
                    var entity = await _navNodeRepository.FindAsync(p => p.Id == dto.Id);
                    if (entity != null)
                    {
                        entity.Update(dto);
                        _navNodeRepository.Modify(entity);
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
                    var entity = await _navNodeRepository.FindAsync(p => p.Id == id);
                    _navNodeRepository.Delete(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }
    }
}