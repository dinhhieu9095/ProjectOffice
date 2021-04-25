using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SurePortal.Core.Kernel.Notifications.Infrastructure.Config
{
    public class NotificationTypeConfiguration : EntityTypeConfiguration<NotificationType>
    {
        public NotificationTypeConfiguration()
        {
            ToTable("Core.NotificationTypes");
        }
    }
}
