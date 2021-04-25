using System.Data.Entity.Migrations;

namespace SurePortal.Core.Kernel.Notifications.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Infrastruture.NotificationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastruture.NotificationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
