namespace DaiPhatDat.WebHost.Modules.Navigation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DaiPhatDat.WebHost.Modules.Navigation.NavigationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Modules\Navigation\Migrations";
        }

        protected override void Seed(DaiPhatDat.WebHost.Modules.Navigation.NavigationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
