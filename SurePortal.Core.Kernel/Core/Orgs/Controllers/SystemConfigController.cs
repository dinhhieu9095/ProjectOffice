using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.JavaScript;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Core.Kernel.Orgs.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace SurePortal.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("_apis/system-configs")]
    public class SystemConfigController : ApiCoreController
    {
        private readonly ISystemConfigServices _systemConfigService;
        public SystemConfigController(
             ILoggerServices loggerServices,
             IUserServices userService,
             IUserDepartmentServices userDepartmentServices,
             ISystemConfigServices systemConfigService
             ) : base(loggerServices, userService, userDepartmentServices)
        {

            _systemConfigService = systemConfigService;
        }
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CrudSystemConfig(CrudSystemConfigInput input)
        {
            switch (input.Action)
            {
                case "insert":
                    var result = await _systemConfigService.AddAsync(input.Value).ConfigureAwait(false);
                    return Ok(result);
                case "update":
                    await _systemConfigService.UpdateAsync(input.Value).ConfigureAwait(false);
                    return Ok(input.Value);
                case "remove":
                    await _systemConfigService.DeleteAsync(input.Key).ConfigureAwait(false);
                    return Ok(new
                    {
                        input.Key
                    });
                default:
                    return BadRequest();
            }
        }
        [Route("data")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllSystemConfigs(DataManager dataManager)
        {
            var paginate = await _systemConfigService.GetPaginationAsync(dataManager).ConfigureAwait(false);
            return Ok(new
            {
                result = paginate.Result,
                count = paginate.Count
            });
        }
    }
}
