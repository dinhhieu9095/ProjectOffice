using System.Data.Entity.Migrations;

namespace DaiPhatDat.Core.Kernel.Notifications.Migrations
{
    public partial class InitialNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.NotificationTypes",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 300),
                    Description = c.String(maxLength: 2000),
                    Template = c.String(),
                    ModuleCode = c.String(maxLength: 100, nullable: false),
                    ActionType = c.Int(nullable: false),
                    ActionTypeName = c.String(maxLength: 100),
                    IsDeleted = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "core.Notifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    ModuleCode = c.String(maxLength: 100, nullable: false),
                    AdditionalData = c.Binary(nullable: true),
                    IsDeleted = c.Boolean(nullable: false),
                    NotificationTypeId = c.Guid(nullable: false),
                    IsRead = c.Boolean(nullable: false),
                    RecipientId = c.Guid(nullable: false),
                    SenderId = c.Guid(),
                    Url = c.String(maxLength: 2000),
                    Subject = c.String(maxLength: 2000),
                    Body = c.String(),
                    SenderEmail = c.String(maxLength: 300),
                    RecipientEmail = c.String(maxLength: 3000),
                    CcEmail = c.String(nullable: true, maxLength: 3000),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.NotificationTypes", t => t.NotificationTypeId, cascadeDelete: true)
                .Index(t => t.NotificationTypeId);

            CreateTable(
                "core.NotificationSettings",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    UserId = c.Guid(nullable: false),
                    IsUrgent = c.Boolean(nullable: false),
                    NotificationTypeId = c.Guid(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("core.Notifications", "NotificationTypeId", "core.NotificationTypes");
            DropIndex("core.Notifications", new[] { "NotificationTypeId" });
            DropTable("core.NotificationSettings");
            DropTable("core.Notifications");
            DropTable("core.NotificationTypes");
        }
    }
}
