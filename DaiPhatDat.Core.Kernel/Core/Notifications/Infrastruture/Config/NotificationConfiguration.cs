using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaiPhatDat.Core.Kernel.Notifications.Infrastructure.Config
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            ToTable("Core.Notifications");
        }
    }
}
