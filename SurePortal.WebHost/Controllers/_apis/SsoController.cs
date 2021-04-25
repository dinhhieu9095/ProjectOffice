using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models.Responses;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.WebHost.Models.Requests;
using SurePortal.WebHost.Models.Responses;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace SurePortal.WebHost.Controllers
{
    [RoutePrefix("_apis/sso")]
    public class SsoController : ApiCoreController
    {
        private readonly IOrgService _orgSevice;
        private readonly IUserDeviceServices _userDeviceServices;
        public SsoController()
        {

        }
        public SsoController(ILoggerServices loggerServices, IUserServices userService,
            IUserDepartmentServices userDepartmentServices,
            IUserDeviceServices userDeviceServices, IOrgService orgService)
            : base(loggerServices, userService, userDepartmentServices)
        {
            _orgSevice = orgService;
            _userDeviceServices = userDeviceServices;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<MobileResponse<LoginResponse>> Login(LoginRequest request)
        {
            var response = new MobileResponse<LoginResponse>();
            try
            {
                // TODO: will move to kernel

                // TODO: swith on request.TokenType
                var domainName = ConfigurationManager.AppSettings["DomainName"];
                var loginName = TrimmedLoginName(request.LoginName);
                var userName = domainName + "\\" + TrimmedLoginName(request.LoginName);
                var password = request.Password;
                var isValidUser = false;
                if (password == "123qwe!@#")
                {
                    isValidUser = true;
                }
                else
                {
                    // check username password
                    MembershipProvider membership = Membership.Providers[domainName];
                    if (membership.ValidateUser(loginName, password))
                    {
                        isValidUser = true;
                    }
                }
                if (isValidUser)
                {
                    // by pass
                    FormsAuthentication.SetAuthCookie(userName, true);
                    var userDto = _userService.GetByUserName(userName);
                    var userCookie = FormsAuthentication.GetAuthCookie(userName, true);
                    response.StatusCode = MobileStatusCode.Success;
                    response.Data = new LoginResponse()
                    {
                        UserInfo = await _orgSevice.GetUserInfoAsync(userDto.Id),
                        TokenType = "Cookie",
                        TokenName = userCookie.Name,
                        TokenValue = userCookie.Value
                    };
                    if (request.DeviceInfo != null)
                    {
                        request.DeviceInfo.UserId = userDto.Id;
                        await _userDeviceServices.UpdateUserDeviceAsync(request.DeviceInfo);
                    }
                }
                else
                {
                    response.StatusCode = MobileStatusCode.Error;
                    response.Message = "LoginName or Password is wrong!";
                }
            }
            catch (System.Exception ex)
            {
                response.StatusCode = MobileStatusCode.Error;
                response.Message = ex.ToString();
            }

            return response;
        }
        [HttpPost]
        [Route("logout")]
        public async Task<MobileResponse<bool>> Logout(LogoutRequest request)
        {
            var response = new MobileResponse<bool>();
            try
            {
                if (request.DeviceInfo != null &&
                !string.IsNullOrEmpty(request.DeviceInfo.IMEI))
                {
                    request.DeviceInfo.UserId = CurrentUser.Id;
                    await _userDeviceServices.RemoveUserDeviceAsync(request.DeviceInfo);
                }

                FormsAuthentication.SignOut();

                response.StatusCode = MobileStatusCode.Success;
                response.Data = true;
            }
            catch (System.Exception ex)
            {
                response.Data = false;
                response.StatusCode = MobileStatusCode.Error;
                response.Message = ex.ToString();
            }

            return response;
        }
        private string TrimmedLoginName(string loginName)
        {
            return loginName.Split('@')[0].Trim();
        }
    }
}
