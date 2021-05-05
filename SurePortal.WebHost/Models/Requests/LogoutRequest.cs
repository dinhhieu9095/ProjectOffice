using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;

namespace DaiPhatDat.WebHost.Models.Requests
{
    public class LogoutRequest
    {
        public UserDeviceDto DeviceInfo { get; set; }
    }
}