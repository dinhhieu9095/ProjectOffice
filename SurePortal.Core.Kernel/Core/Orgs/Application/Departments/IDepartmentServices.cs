using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface IDepartmentServices
    {
        Task<IReadOnlyList<DepartmentDto>> GetDepartmentsAsync(bool clearCache = false);
        Task<List<DepartmentDto>> GetHierarchyDepartmentsAsync(Guid parentId = default);
        Task<List<DepartmentDto>> GetLiveHierarchyDepartmentsAsync(Guid parentId = default);
        Task<List<DepartmentDto>> GetDislayNameHierarchyDepartmentsAsync(Guid rootId = default, char prefix = ' ');
        Task<DepartmentDto> GetAsync(Guid id);
        Task<Guid> CreateAsync(CreateDepartmentDto item);
        Task<bool> UpdateAsync(UpdateDepartmentDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<UpdateDepartmentDto> GetUpdateDepartmentAsync(Guid id);
        List<DepartmentDto> GetChildren(IReadOnlyList<DepartmentDto> departmentDTOs, Guid? id);
    }
}