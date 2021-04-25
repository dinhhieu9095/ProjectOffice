using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.JavaScript;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Notifications.Application.NotificationTypes;
using SurePortal.Core.Kernel.Notifications.Models;
using SurePortal.Core.Kernel.Orgs.Application;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SurePortal.Core.Kernel.Notifications.Controller
{
    [RoutePrefix("_apis/notification-types")]
    public class NotificationTypeController : ApiCoreController
    {
        private readonly INotificationTypeService _notificationTypeService;
        public NotificationTypeController(
             ILoggerServices loggerServices,
             IUserServices userService,
             IUserDepartmentServices userDepartmentServices,
             INotificationTypeService notificationTypeService
             ) : base(loggerServices, userService, userDepartmentServices)
        {

            _notificationTypeService = notificationTypeService;
        }
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CrudNotificationType(CrudNotificationTypeInput input)
        {
            switch (input.Action)
            {
                case "insert":
                    var result = await _notificationTypeService.AddAsync(input.Value).ConfigureAwait(false);
                    return Ok(result);
                case "update":
                    await _notificationTypeService.UpdateAsync(input.Value).ConfigureAwait(false);
                    return Ok(input.Value);
                case "remove":
                    await _notificationTypeService.DeleteAsync(Guid.Parse(input.Key)).ConfigureAwait(false);
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
        public async Task<IHttpActionResult> GetAllNotificationTypes(DataManager dataManager)
        {
            var paginate = await _notificationTypeService.GetPaginationAsync(dataManager).ConfigureAwait(false);
            return Ok(new
            {
                result = paginate.Result,
                count = paginate.Count
            });
        }

        [Route("{id:guid}/copy")]
        [HttpPost]
        public async Task<IHttpActionResult> CopyNotificationTypeAsync(Guid id)
        {
            var result = await _notificationTypeService.CopyAsync(id).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
