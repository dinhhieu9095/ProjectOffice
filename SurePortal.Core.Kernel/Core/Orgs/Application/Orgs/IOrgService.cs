using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public interface IOrgService
    {
        Task<IReadOnlyList<DepartmentInfo>> GetActiveDepartmentsAsync();
        Task<IReadOnlyList<UserInfo>> GetAllUserInfoAsync();
        Task<Pagination<UserInfo>> SearchUsersAsync(UserFilterInput filter);
        Task<DepartmentInfo> GetDepartmentInfoAsync(Guid departmentId);
        Task<IReadOnlyList<Guid>> GetRecuresiveDeptIDAsync(Guid departmentId);
        Task<UserInfo> GetUserInfoAsync(Guid userId, bool includeDepartment = false);
        Task<IReadOnlyList<DepartmentTypeDto>> GetDepartmentTypesAsync();
        Task<Guid> CreateUserAsync(CreateUserDto createUserDto);
        Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<IReadOnlyList<UserJobTitleDto>> GetJobtitlesAsync();
        Task<bool> DeleteUserDepartmentAsync(Guid userId, Guid deptId);
        Task<IList<JsTreeViewModel>> GetOrgTreeAsync();
        Task<IList<JsTreeViewModel>> GetOrgTreeNoCachedAsync();
        Task<bool> ImportOrgAsync(string importDataFilePath);
        Task<bool> UpdateAdInfo(string userName, string employeeID, string mail, string ext, string mobile, byte[] avatar);
        Task<IList<JsTreeViewModel>> GetOrgUserTree(bool isGetUser = true);
    }
}