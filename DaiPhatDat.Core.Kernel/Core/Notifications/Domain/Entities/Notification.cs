using DaiPhatDat.Core.Kernel.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Entities
{
    [Table("Notifications", Schema = "Core")]
    public class Notification : BaseEntity
    {
        public string ModuleCode { get; private set; }

        public byte[] AdditionalData { get; private set; }

        public bool IsDeleted { get; private set; }

        public Guid? NotificationTypeId { get; private set; }

        public virtual NotificationType NotificationType { get; protected set; }

        public bool IsRead { get; private set; }

        public Guid RecipientId { get; private set; }

        public Guid? SenderId { get; private set; }
        public Guid? ObjectId { get; private set; }

        public string Url { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public string SenderEmail { get; private set; }

        public string RecipientEmail { get; private set; }

        public string CcEmail { get; private set; }


        public static Notification Create(Guid? notificationTypeId, string moduleCode, Guid? senderId,
            Guid recipientId, Guid? objectId, string url, string subject, string body,
            byte[] additionalData = null)
        {
            return new Notification()
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                AdditionalData = additionalData,
                IsRead = false,
                NotificationTypeId = notificationTypeId,
                RecipientId = recipientId,
                SenderId = senderId,
                ObjectId = objectId,
                Url = url,
                Subject = subject,
                Body = body,
                ModuleCode = moduleCode
            };
        }

        public void UpdateStatus(bool isRead, bool isDeleted)
        {
            IsRead = isRead;
            IsDeleted = isDeleted;
            ModifiedDate = DateTime.Now;
        }

    }
}
