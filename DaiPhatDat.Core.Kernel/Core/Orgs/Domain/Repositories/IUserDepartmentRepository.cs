using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Infrastructure
{
    public interface IUserDepartmentRepository : IRepository<UserDepartment>
    {
        Task<IList<UserDepartment>> GetAllUserDepartments();
        UserDepartment GetUserDepartment(Guid userId, Guid deptId);
        Task<IList<ReadUserDepartment>> GetUserDepartments(Guid deptId, int pageIndex,
            int pageSize, string filterKeyword);
        IList<UserDepartment> GetUserDepartmentsByDept(Guid deptId);
        IList<UserDepartment> GetUserDepartmentsByUser(Guid userId);
    }
}