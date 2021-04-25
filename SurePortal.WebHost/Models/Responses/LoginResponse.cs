using SurePortal.Core.Kernel.Orgs.Application.Dto;

namespace SurePortal.WebHost.Models.Responses
{
    public class LoginResponse
    {
        public UserInfo UserInfo { get; set; }

        public string TokenType { get; set; }

        public string TokenName { get; set; }

        public string TokenValue { get; set; }
    }
}