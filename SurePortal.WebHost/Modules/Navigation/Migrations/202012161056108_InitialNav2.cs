namespace SurePortal.WebHost.Modules.Navigation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialNav2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("nav.Menu", "GroupOrUsers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("nav.Menu", "GroupOrUsers");
        }
    }
}
