using SurePortal.Core.Kernel.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Notifications.Domain.Entities
{
    [Table("NotificationSettings", Schema = "Core")]
    public class NotificationSetting : BaseEntity
    {
        public Guid UserId { get; private set; }

        public bool IsUrgent { get; private set; }

        public Guid NotificationTypeId { get; private set; }

        public static NotificationSetting Create(Guid userId, Guid notificationTypeId, bool isUrgent)
        {
            return new NotificationSetting()
            {
                UserId = userId,
                NotificationTypeId = notificationTypeId,
                IsUrgent = isUrgent,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };
        }

        public void Update(Guid notificationTypeId, bool isUrgent)
        {
            NotificationTypeId = notificationTypeId;
            IsUrgent = isUrgent;
            ModifiedDate = DateTime.Now;
        }
    }
}
