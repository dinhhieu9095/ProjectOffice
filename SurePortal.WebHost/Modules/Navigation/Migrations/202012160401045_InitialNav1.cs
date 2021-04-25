namespace SurePortal.WebHost.Modules.Navigation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialNav1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("nav.Menu", "Roles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("nav.Menu", "Roles");
        }
    }
}
