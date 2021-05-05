using AutoMapper;
using AutoMapper.QueryableExtensions;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    /// <summary>
    /// Lớp dịch vụ cung cấp thông tin phòng ban
    /// </summary>
    public class DepartmentServices : IDepartmentServices
    {
        #region Static Attributes

        private static List<DepartmentDto> _departments = null;

        #endregion Static Attributes

        #region Attributes

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IDepartmentRepository _deparmentRepository;
        private readonly IMapper _mapper;

        #endregion Attributes

        #region Constructors

        public DepartmentServices(
          IDbContextScopeFactory dbContextScopeFactory,
          IDepartmentRepository departmentRepository,
          IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _deparmentRepository = departmentRepository;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods
        public async Task<IReadOnlyList<DepartmentDto>> GetDepartmentsAsync(bool clearCache = false)
        {
            if (_departments == null || clearCache)
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    _departments = await _deparmentRepository
                    .GetAll()
                    .AsNoTracking()
                    .OrderBy(d => d.OrderNumber)
                    .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
            }
            return _departments;
        }
        public async Task<List<DepartmentDto>> GetHierarchyDepartmentsAsync(Guid parentId = default(Guid))
        {
            List<DepartmentDto> rootDepartments;
            var all = await GetDepartmentsAsync();
            if (parentId == Guid.Empty)
            {
                rootDepartments = all
                    .Where(w => w.IsActive == true && w.ParentID == null || w.ParentID == Guid.Empty)
                    .OrderBy(o => o.OrderNumber).ToList();
            }
            else
            {
                rootDepartments = all
                   .Where(w => w.IsActive == true && w.ParentID == parentId)
                   .OrderBy(o => o.OrderNumber).ToList();
            }
            foreach (var dept in rootDepartments)
            {
                dept.Children = await GetHierarchyDepartmentsAsync(dept.Id);
            }

            return rootDepartments;
        }
        public async Task<List<DepartmentDto>> GetLiveHierarchyDepartmentsAsync(Guid parentId = default)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {

                List<DepartmentDto> rootDepartments;
                var departments = await _deparmentRepository
                    .GetAll()
                    .AsNoTracking()
                    .OrderBy(o => o.OrderNumber)
                    .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                if (parentId == Guid.Empty)
                {
                    rootDepartments = departments
                        .Where(w => w.IsActive == true && w.ParentID == null || w.ParentID == Guid.Empty)
                        .OrderBy(o => o.OrderNumber).ToList();
                }
                else
                {
                    rootDepartments = departments
                       .Where(w => w.IsActive == true && w.ParentID == parentId)
                       .OrderBy(o => o.OrderNumber).ToList();
                }
                var tasks = from department in rootDepartments
                            select SetChildDepartmentsAsync(departments, department);
                await Task.WhenAll(tasks);
                return rootDepartments;
            }
        }
        public async Task<List<DepartmentDto>> GetDislayNameHierarchyDepartmentsAsync(
            Guid rootId = default,
            char prefix = ' ')
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _deparmentRepository.GetHierarchyDepartmentsAsync(rootId, prefix);
            }
        }
        public async Task<DepartmentDto> GetAsync(Guid id)
        {
            var allDepartmentDtos = await GetDepartmentsAsync();
            return allDepartmentDtos.FirstOrDefault(f => f.Id == id);
        }

        public async Task<Guid> CreateAsync(CreateDepartmentDto item)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var entity = Department.Create(item.Name,
                    item.Code, item.ParentID, item.DeptTypeID, item.DeptGroupID,
                    item.OrderNumber.HasValue ? item.OrderNumber.Value : 99, item.IsPrint,
                    item.IsShow, item.CreatedBy);
                _deparmentRepository.Add(entity);
                await scope.SaveChangesAsync();
                return entity.Id;
            }
        }

        public async Task<bool> UpdateAsync(UpdateDepartmentDto item)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var model = await _deparmentRepository.FindAsync(f => f.Id == item.Id);
                model.Update(item.Name, item.Code, item.OrderNumber.HasValue ? item.OrderNumber.Value : 99,
                    item.DeptTypeID, item.ParentID,
                     item.DeptGroupID, item.IsShow, item.IsPrint, item.ModifiedBy);
                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var model = await _deparmentRepository.FindAsync(f => f.Id == id);
                model.UpdateActive(false, false, false);
                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<UpdateDepartmentDto> GetUpdateDepartmentAsync(Guid id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var model = await _deparmentRepository.FindAsync(f => f.Id == id);

                return new UpdateDepartmentDto()
                {
                    Id = model.Id,
                    Code = model.Code,
                    DeptGroupID = model.RootDBID.HasValue ? model.RootDBID.Value : Guid.Empty,
                    DeptTypeID = model.DeptTypeID,
                    IsPrint = model.IsPrint == true,
                    IsShow = model.IsShow,
                    Name = model.Name,
                    OrderNumber = model.OrderNumber,
                    ParentID = model.ParentID.HasValue ? model.ParentID.Value : Guid.Empty
                };
            }
        }

        public List<DepartmentDto> GetChildren(IReadOnlyList<DepartmentDto> departmentDTOs, Guid? id)
        {
            return departmentDTOs
                .Where(t => t.ParentID == id)
                .ToList()
                .Union(departmentDTOs
                    .Where(t => t.ParentID == id)
                    .ToList()
                    .SelectMany(x => GetChildren(departmentDTOs, x.Id)))
                .ToList();
        }

        #endregion


        private async Task SetChildDepartmentsAsync(IList<DepartmentDto> departments, DepartmentDto department)
        {
            List<DepartmentDto> childDepartments = departments
               .Where(w => w.IsActive == true && w.ParentID == department.Id)
               .OrderBy(o => o.OrderNumber).ToList();
            if (childDepartments.Any())
            {
                var tasks = from childDepartment in childDepartments
                            select SetChildDepartmentsAsync(departments, childDepartment);
                await Task.WhenAll(tasks);
            }
            department.Children = childDepartments;
        }
    }
}