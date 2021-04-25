using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using System;

namespace SurePortal.Core.Kernel.Notifications.Application.NotificationTypes
{
    public class NotificationTypeModel : IMapping<NotificationTypeDto>
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
