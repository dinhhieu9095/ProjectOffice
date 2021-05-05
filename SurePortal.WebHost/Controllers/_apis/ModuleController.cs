using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Models.Responses;
using System.Collections.Generic;
using System.Web.Http;

namespace DaiPhatDat.WebHost.Controllers
{
    [RoutePrefix("_apis/module")]
    public class ModuleController : ApiCoreController
    {
        [Route("")]
        public MobileResponse<List<string>> Get()
        {
            return MobileResponse<List<string>>.Create(MobileStatusCode.Success,
                "", new List<string>() {
                SurePortalModules.BW.ToString()
                });
        }
    }
}
