namespace SurePortal.WebHost.Modules.Navigation.Migrations
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
                        Code = c.String(),
                        NavNodeId = c.Guid(),
                        ParentId = c.Guid(),
                        ModuleId = c.Guid(),
                        TypeModule = c.Int(),
                        Layout = c.String(),
                        Status = c.Byte(nullable: false),
                        Target = c.String(),
                        Icon = c.String(),
                        Order = c.Int(nullable: false),
                        Name = c.String(),
                        URL = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        ActiveFag = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("nav.NavNode", t => t.NavNodeId)
                .Index(t => t.NavNodeId);
            
            CreateTable(
                "nav.NavNode",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Byte(nullable: false),
                        Areas = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Params = c.String(),
                        ResourceId = c.String(),
                        NameEN = c.String(),
                        Description = c.String(),
                        Method = c.String(),
                        Name = c.String(),
                        URL = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        ActiveFag = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "nav.MenuRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        MenuId = c.Guid(nullable: false),
                        Name = c.String(),
                        URL = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        ActiveFag = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("nav.Menu", "NavNodeId", "nav.NavNode");
            DropIndex("nav.Menu", new[] { "NavNodeId" });
            DropTable("nav.MenuRole");
            DropTable("nav.NavNode");
            DropTable("nav.Menu");
        }
    }
}
