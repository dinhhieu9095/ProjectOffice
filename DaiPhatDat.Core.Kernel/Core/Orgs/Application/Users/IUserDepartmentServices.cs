using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public interface IUserDepartmentServices
    {
        Task<IReadOnlyList<UserDepartmentDto>> GetCachedUserDepartmentDtos(bool clearCache = false);
        IList<UserDepartmentDto> GetCachedUserDepartmentsByUser(Guid userId);
        Task<IList<ReadUserDepartmentDto>> GetUserDepartmentDtos(Guid deptId, int pageIndex,
            int pageSize, string filterKeyword);
        IList<UserDepartmentDto> GetUserDepartmentsByDept(Guid userId);
        IList<UserDepartmentDto> GetUserDepartmentsByUser(Guid userId);
    }
}