using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Infrastructure
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<List<Department>> GetAllAsync();
        Task<List<DepartmentDto>> GetHierarchyDepartmentsAsync(Guid rootId = default, char prefix = ' ');
    }
}