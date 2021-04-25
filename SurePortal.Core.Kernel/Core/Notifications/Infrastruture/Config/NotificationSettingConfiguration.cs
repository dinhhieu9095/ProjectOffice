using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SurePortal.Core.Kernel.Notifications.Infrastructure.Config
{
    public class NotificationSettingConfiguration : EntityTypeConfiguration<NotificationSetting>
    {
        public NotificationSettingConfiguration()
        {
            ToTable("Core.NotificationSettings");
        }
    }
}
