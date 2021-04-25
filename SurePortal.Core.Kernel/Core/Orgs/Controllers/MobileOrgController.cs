using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models.Responses;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SurePortal.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("_apis/m/org")]
    public class MobileOrgController : ApiCoreController
    {
        private readonly IOrgService _orgService;

        public MobileOrgController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices,
            IOrgService orgService) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _orgService = orgService;
        }

        [Route("get-users")]
        [HttpGet]
        public async Task<MobileResponse<IReadOnlyList<UserInfo>>> GetUsers()
        {
            try
            {
                var output = await _orgService.GetAllUserInfoAsync().ConfigureAwait(false);
                return MobileResponse<IReadOnlyList<UserInfo>>
                    .Create(MobileStatusCode.Success, null, output);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<IReadOnlyList<UserInfo>>
                    .Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }
    }
}
