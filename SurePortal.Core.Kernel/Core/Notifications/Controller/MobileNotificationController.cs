using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Domain.ValueObjects;
using SurePortal.Core.Kernel.Firebase.Application;
using SurePortal.Core.Kernel.Firebase.Models;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models.Responses;
using SurePortal.Core.Kernel.Notifications.Application;
using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using SurePortal.Core.Kernel.Notifications.Models;
using SurePortal.Core.Kernel.Orgs.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SurePortal.Core.Kernel.Notifications.Controller
{
    [RoutePrefix("_apis/m/notification")]
    public class MobileNotificationController : ApiCoreController
    {
        private readonly INotificationServices _notificationServices;
        private readonly IUserDeviceServices _userDeviceServices;
        public MobileNotificationController(ILoggerServices loggerServices,
            IUserServices userService, IUserDepartmentServices userDepartmentServices,
            INotificationServices notificationServices, IUserDeviceServices userDeviceServices) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _notificationServices = notificationServices;
            _userDeviceServices = userDeviceServices;
        }

        [Route("")]
        [HttpGet]
        public async Task<MobileResponse<IReadOnlyList<NotificationDto>>> GetMessages()
        {

            try
            {

                var result = await _notificationServices
                    .GetMessagesAsync(CurrentUser.Id, NotificationActionTypes.Web);
                return MobileResponse<IReadOnlyList<NotificationDto>>
                    .Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<IReadOnlyList<NotificationDto>>
                    .Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }
        [Route("search")]
        [HttpGet]
        public async Task<MobileResponse<IReadOnlyList<NotificationDto>>> Search(NotificationActionTypes actionType,
            string moduleCode)
        {
            try
            {
                var result = await _notificationServices.SearchListAsync(CurrentUser.Id, moduleCode, actionType);
                return MobileResponse<IReadOnlyList<NotificationDto>>
                    .Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<IReadOnlyList<NotificationDto>>
                    .Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }

        [Route("delete")]
        [HttpPost]
        public async Task<MobileResponse<bool>> Delete(UpdateNotificationInput input)
        {
            try
            {
                var result = await _notificationServices.DeleteAsync(CurrentUser.Id, input.Ids);
                return MobileResponse<bool>.Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<bool>.Create(MobileStatusCode.Error, ex.ToString(), false);
            }
        }

        [Route("read")]
        [HttpPost]
        public async Task<MobileResponse<bool>> Read(UpdateNotificationInput input)
        {
            try
            {
                var result = await _notificationServices.UpdateReadStatusAsync(CurrentUser.Id, input.Ids, true);
                return MobileResponse<bool>.Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<bool>.Create(MobileStatusCode.Error, ex.ToString(), false);
            }
        }

        [Route("test-display-firebase")]
        [HttpPost]
        public async Task<MobileResponse<SendMessageResponse>> TestDisplayFirebase(SurePortalModules module,
            string title, string body,
            string imageUrl)
        {
            try
            {
                var firebaseService = new FirebaseServices(_userDeviceServices, _loggerServices);
                var message = SendMessageData.CreateDisplayMessage(title, body, imageUrl, module);
                var result = await firebaseService.SendMessageAsync(message, new List<Guid>() { CurrentUser.Id });

                return MobileResponse<SendMessageResponse>.Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<SendMessageResponse>.Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }

        [Route("test-syncdata-firebase")]
        [HttpPost]
        public async Task<MobileResponse<SendMessageResponse>> TestSyncDataFirebase(SurePortalModules module,
            string body, string imageUrl, Guid? objectId)
        {
            try
            {
                var firebaseService = new FirebaseServices(_userDeviceServices, _loggerServices);
                var message = SendMessageData.CreateSyncDataMessage(body, imageUrl, module, objectId);
                var result = await firebaseService.SendMessageAsync(message, new List<Guid>() { CurrentUser.Id });
                return MobileResponse<SendMessageResponse>.Create(MobileStatusCode.Success, null, result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<SendMessageResponse>.Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }
    }
}
