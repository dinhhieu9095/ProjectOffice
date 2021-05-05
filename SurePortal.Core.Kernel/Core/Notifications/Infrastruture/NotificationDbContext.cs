using DaiPhatDat.Core.Kernel.Notifications.Infrastructure.Config;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DaiPhatDat.Core.Kernel.Notifications.Infrastruture
{
    public class NotificationDbContext : Context, IContext
    {
        public NotificationDbContext() : base()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("core");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new NotificationTypeConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new NotificationSettingConfiguration());
            Database.SetInitializer<NotificationDbContext>(null);
        }
    }
}
