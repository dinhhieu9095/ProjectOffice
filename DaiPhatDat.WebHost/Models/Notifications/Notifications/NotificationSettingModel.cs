using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Notifications.Application.NotificationTypes;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using System;

namespace SurePortal.WebHost.Models.Notifications.Notifications
{
    public class NotificationSettingModel : IMapping<NotificationSettingDto>
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }

        public NotificationTypeModel NotificationType { get; set; }
    }

    public class CreateNotificationSettingModel
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }
    }

    public class UpdateNotificationSettingModel
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }

    }

}
