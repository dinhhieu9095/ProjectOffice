using DaiPhatDat.Core.Kernel.Application;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Linq;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Notifications.Application;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.WebHost.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DaiPhatDat.WebHost.Controllers
{
    [Authorize]
    public class HomeController : CoreController
    {
        public HomeController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices, INotificationServices notificationServices)
            : base(loggerServices, userService, userDepartmentServices)
        {
            _notificationServices = notificationServices;
        }
        private INotificationServices _notificationServices;
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
                if (authenticatedUserDepartments != null)
                    ViewBag.AuthenticatedUserDepartment = authenticatedUserDepartments.FirstOrDefault();
                else ViewBag.AuthenticatedUserDepartment = null;
            }
            ViewBag.TotalNofi = _notificationServices.TotalNotificationAsync(CurrentUser.Id, "", NotificationActionTypes.Web);

            return PartialView(currentUser);
        }

        [HttpGet]
        public async Task<ActionResult> GetNotifications(int size)
        {
            var data = await _notificationServices.SearchListAsync(CurrentUser.Id, "", NotificationActionTypes.Web, size);

            return PartialView("UserNotification", data);
        }
        [HttpGet]
        public ActionResult LoadURLNotification(Guid Id)
        {
            var noti = _notificationServices.GetById(Id);
            if (noti != null && !string.IsNullOrEmpty(noti.Url))
            {
                return Redirect(noti.Url);
            }
            return Redirect("");
        }
    }
}