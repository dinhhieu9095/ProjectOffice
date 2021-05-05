using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using System;

namespace DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes
{
    public class NotificationTypeDto : IMapping<NotificationType>
    {
        public Guid Id { get; set; }
        public string ModuleCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Template { get; set; }

        public NotificationActionTypes ActionType { get; set; }

        public string ActionTypeName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
