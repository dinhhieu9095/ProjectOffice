using SurePortal.Core.Kernel.Application;
using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.WebHost.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SurePortal.WebHost.Controllers
{
    [Authorize]
    public class HomeController : CoreController
    {
        public HomeController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices)
            : base(loggerServices, userService, userDepartmentServices)
        {
        }
        [Route("index")]
        public ActionResult Index()
        {
            var homeUrl = AppSettings.HomeUrl;
            if (!string.IsNullOrEmpty(homeUrl))
            {
                return Redirect(homeUrl);
            }
            return View(CurrentUser);
        }
        [HttpGet]
        public ActionResult TopNavigation()
        {

            var currentUser = new CurrentUserViewModel
            {
                ID = CurrentUser.Id,
                FullName = CurrentUser.FullName,
                AccountName = CurrentUser.AccountName
            };

            var currentDBDeptID = Guid.Empty;
            var authenticatedUserDepartmentCookie = Request.Cookies["AuthenticatedUserDepartment"];
            if (authenticatedUserDepartmentCookie != null)
                currentDBDeptID = new Guid(authenticatedUserDepartmentCookie.Value);


            var authenticatedUserDepartments = _userDepartmentServices
            .GetCachedUserDepartmentsByUser(currentUser.ID);

            ViewBag.AuthenticatedUserDepartments = authenticatedUserDepartments;
            if (currentDBDeptID != Guid.Empty)
            {
                ViewBag.AuthenticatedUserDepartment =
                    authenticatedUserDepartments.FirstOrDefault(x => x.DeptID == currentDBDeptID);
                if (ViewBag.AuthenticatedUserDepartment == null)
                    ViewBag.AuthenticatedUserDepartment = authenticatedUserDepartments.FirstOrDefault();
            }
            else
            {
                ViewBag.AuthenticatedUserDepartment = authenticatedUserDepartments.FirstOrDefault();
            }


            return PartialView(currentUser);
        }
    }
}