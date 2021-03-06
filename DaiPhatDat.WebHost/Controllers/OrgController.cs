using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.WebHost.Controllers
{
    [Authorize]
    public class OrgController : CoreController
    {
        public OrgController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IOrgService orgService) : base(loggerServices, userService, userDepartmentServices)
        {
            _orgService = orgService;
        }

        private readonly IOrgService _orgService;

        [HttpGet]
        public async Task<JsonResult> GetOrgUserChart()
        {
            IList<JsTreeViewModel> model = new List<JsTreeViewModel>();
            try
            {
                model = await _orgService.GetOrgUserTree();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetOrgChart()
        {
            IList<JsTreeViewModel> model = new List<JsTreeViewModel>();
            try
            {
                model = await _orgService.GetOrgUserTree(false);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}