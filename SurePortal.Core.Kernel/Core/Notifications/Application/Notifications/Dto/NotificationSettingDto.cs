using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Application.NotificationTypes;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using System;

namespace SurePortal.Core.Kernel.Notifications.Application.Dto
{
    public class NotificationSettingDto : IMapping<NotificationSetting>
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }

        public NotificationTypeDto NotificationType { get; set; }
    }

    public class CreateNotificationSettingDto
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }
    }

    public class UpdateNotificationSettingDto
    {
        public Guid UserId { get; set; }

        public bool IsUrgent { get; set; }

        public Guid NotificationTypeId { get; set; }

    }

}
