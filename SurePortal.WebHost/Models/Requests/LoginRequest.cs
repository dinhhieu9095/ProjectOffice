using SurePortal.Core.Kernel.Orgs.Application.Dto;

namespace SurePortal.WebHost.Models.Requests
{
    public class LoginRequest
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string TokenType { get; set; }
        public UserDeviceDto DeviceInfo { get; set; }
        /// <summary>
        /// TODO: for QR login
        /// </summary>
        public string LoginToken { get; set; }
    }
}