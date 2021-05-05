using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using System;

namespace SurePortal.WebHost.Models.Notifications.Notifications
{
    public class NotificationModel : IMapping<NotificationDto>
    {
        public Guid Id { get; set; }

        public string ModuleCode { get; set; }

        public bool IsDeleted { get; set; }

        public Guid NotificationTypeId { get; set; }

        public bool IsRead { get; set; }

        public Guid RecipientId { get; set; }

        public Guid? SenderId { get; set; }
        public Guid? ObjectId { get; set; }

        public string Url { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string SenderEmail { get; set; }

        public string RecipientEmail { get; set; }

        public string CcEmail { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class CreateNotificationModel
    {
        public string ModuleCode { get; set; }

        public byte[] AdditionalData { get; set; }

        public Guid NotificationTypeId { get; set; }

        public Guid RecipientId { get; set; }

        public Guid? SenderId { get; set; }
        public Guid ObjectId { get; set; }

        public string Url { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string SenderEmail { get; set; }

        public string RecipientEmail { get; set; }

        public string CcEmail { get; set; }
    }
}
