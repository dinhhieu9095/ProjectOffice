namespace SurePortal.WebHost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialNav : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "nav.Menu",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        ModuleId = c.Guid(),
                        Layout = c.Byte(nullable: false),
                        Status = c.Byte(nullable: false),
                        Areas = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        ResourceID = c.String(),
                        NameVI = c.String(),
                        NameEN = c.String(),
                        Icon = c.String(),
                        OrderNumber = c.Int(nullable: false),
                        Description = c.String(),
                        Method = c.String(),
                        Name = c.String(),
                        URL = c.String(),
                        CreateBy = c.String(),
                        UpdateBy = c.String(),
                        ActiveFag = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("nav.Menu");
        }
    }
}
