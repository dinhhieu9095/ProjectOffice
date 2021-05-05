using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Notifications.Application.Dto
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
