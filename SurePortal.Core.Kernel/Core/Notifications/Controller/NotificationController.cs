using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Notifications.Application;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Notifications.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DaiPhatDat.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("api/notification")]
    public class NotificationController : CoreController
    {
        private readonly INotificationServices _notificationServices;

        public NotificationController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices,
            INotificationServices notificationServices) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _notificationServices = notificationServices;
        }

        [Route("")]
        [HttpGet]
        public async Task<JsonResult> GetMessages()
        {
            var messages = await _notificationServices.GetMessagesAsync(CurrentUser.Id, NotificationActionTypes.Web);
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
        [Route("search")]
        [HttpGet]
        public async Task<JsonResult> Search(string moduleCode, NotificationActionTypes actionType)
        {
            var messages = await _notificationServices.SearchListAsync(CurrentUser.Id, moduleCode, actionType);
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [Route("read")]
        [HttpPost]
        public async Task<JsonResult> UpdateStatus(UpdateNotificationInput input)
        {
            var messages = await _notificationServices.UpdateReadStatusAsync(CurrentUser.Id, input.Ids, true);
            return Json(messages, JsonRequestBehavior.AllowGet);
        }


        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> DeleteAll(UpdateNotificationInput input)
        {
            var messages = await _notificationServices.DeleteAsync(CurrentUser.Id, input.Ids);
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}