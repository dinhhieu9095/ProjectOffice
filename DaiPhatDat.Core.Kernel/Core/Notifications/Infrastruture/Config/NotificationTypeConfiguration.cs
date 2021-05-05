using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaiPhatDat.Core.Kernel.Notifications.Infrastructure.Config
{
    public class NotificationTypeConfiguration : EntityTypeConfiguration<NotificationType>
    {
        public NotificationTypeConfiguration()
        {
            ToTable("Core.NotificationTypes");
        }
    }
}
