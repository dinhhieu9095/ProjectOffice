using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<List<Department>> GetAllAsync();
        Task<List<DepartmentDto>> GetHierarchyDepartmentsAsync(Guid rootId = default, char prefix = ' ');
    }
}