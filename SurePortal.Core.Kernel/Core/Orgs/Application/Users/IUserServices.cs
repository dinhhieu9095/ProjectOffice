using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface IUserServices
    {
        IReadOnlyList<UserDto> GetUsers(bool clearCache = false);
        UserDto GetByUserName(string userName);
        UserDto GetById(Guid userId);
        IList<UserDto> GetUsers(int pageIndex, int pageSize, string filterKeyword);
        byte[] GetAvatarContent(Guid id);
        List<string> GetUserPermission(string userName);
        IReadOnlyList<UserDto> GetUserByRoles(List<Guid> roleId);
    }
}