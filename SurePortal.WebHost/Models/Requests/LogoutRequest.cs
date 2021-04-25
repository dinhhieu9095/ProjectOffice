using SurePortal.Core.Kernel.Orgs.Application.Dto;

namespace SurePortal.WebHost.Models.Requests
{
    public class LogoutRequest
    {
        public UserDeviceDto DeviceInfo { get; set; }
    }
}