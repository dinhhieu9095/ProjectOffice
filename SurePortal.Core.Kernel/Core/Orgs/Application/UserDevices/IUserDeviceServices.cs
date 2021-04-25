using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface IUserDeviceServices
    {
        Task<IReadOnlyList<UserDeviceDto>> GetUserDevicesAsync(Guid userId);
        Task RemoveUserDeviceAsync(UserDeviceDto userDeviceDto);
        Task UpdateUserDeviceAsync(UserDeviceDto userDeviceDto);
        Task<IReadOnlyList<UserDeviceInfoDto>> GetUserDeviceInfosAsync(Guid userId);
        Task<IReadOnlyList<UserDeviceInfoDto>> GetUserDeviceInfosAsync(List<Guid> userIds);
    }
}