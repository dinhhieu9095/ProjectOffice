using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Orgs.Application;
using System;
using System.Web.Http;

namespace SurePortal.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("_apis/user")]
    public class UserController : ApiCoreController
    {
        public UserController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices) :
            base(loggerServices, userService, userDepartmentServices)
        {
        }

        [Route("avatar")]
        [HttpGet]
        public ImageResult Avatar(Guid id)
        {
            var avatar = _userService.GetAvatarContent(id);
            if (avatar == null)
            {
                avatar = System.IO.File
                    .ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img/avatar.png"));
            }
            return new ImageResult(avatar);
        }
    }
}