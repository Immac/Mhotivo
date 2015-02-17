namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gabriel_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        NotificationTypeId = c.Int(nullable: false, identity: true),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.NotificationTypeId);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DeliverDate = c.DateTime(nullable: false),
                        Points = c.Single(nullable: false),
                        AcademicYearDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYearDetails", t => t.AcademicYearDetail_Id)
                .Index(t => t.AcademicYearDetail_Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        NotificationId = c.Long(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.UserId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.People", "UserId_Id", c => c.Int());
            AddColumn("dbo.Notifications", "NotificationName", c => c.String());
            AddColumn("dbo.Notifications", "IdGradeAreaUserGeneralSelected", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "SendingEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notifications", "NotificationCreator_Id", c => c.Int());
            AddColumn("dbo.Notifications", "NotificationType_NotificationTypeId", c => c.Int());
            CreateIndex("dbo.People", "UserId_Id");
            CreateIndex("dbo.Notifications", "NotificationCreator_Id");
            CreateIndex("dbo.Notifications", "NotificationType_NotificationTypeId");
            AddForeignKey("dbo.Notifications", "NotificationCreator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Notifications", "NotificationType_NotificationTypeId", "dbo.NotificationTypes", "NotificationTypeId");
            AddForeignKey("dbo.People", "UserId_Id", "dbo.Users", "Id");
            DropColumn("dbo.AcademicYearDetails", "TeacherStartDate");
            DropColumn("dbo.AcademicYearDetails", "TeacherEndDate");
            DropColumn("dbo.Notifications", "EventName");
            DropColumn("dbo.Notifications", "From");
            DropColumn("dbo.Notifications", "To");
            DropColumn("dbo.Notifications", "WithCopyTo");
            DropColumn("dbo.Notifications", "WithHiddenCopyTo");
            DropColumn("dbo.Notifications", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Subject", c => c.String());
            AddColumn("dbo.Notifications", "WithHiddenCopyTo", c => c.String());
            AddColumn("dbo.Notifications", "WithCopyTo", c => c.String());
            AddColumn("dbo.Notifications", "To", c => c.String());
            AddColumn("dbo.Notifications", "From", c => c.String());
            AddColumn("dbo.Notifications", "EventName", c => c.String());
            AddColumn("dbo.AcademicYearDetails", "TeacherEndDate", c => c.DateTime());
            AddColumn("dbo.AcademicYearDetails", "TeacherStartDate", c => c.DateTime());
            DropForeignKey("dbo.Homework", "AcademicYearDetail_Id", "dbo.AcademicYearDetails");
            DropForeignKey("dbo.People", "UserId_Id", "dbo.Users");
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "NotificationType_NotificationTypeId", "dbo.NotificationTypes");
            DropForeignKey("dbo.Notifications", "NotificationCreator_Id", "dbo.Users");
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.Homework", new[] { "AcademicYearDetail_Id" });
            DropIndex("dbo.Notifications", new[] { "NotificationType_NotificationTypeId" });
            DropIndex("dbo.Notifications", new[] { "NotificationCreator_Id" });
            DropIndex("dbo.People", new[] { "UserId_Id" });
            DropColumn("dbo.Notifications", "NotificationType_NotificationTypeId");
            DropColumn("dbo.Notifications", "NotificationCreator_Id");
            DropColumn("dbo.Notifications", "SendingEmail");
            DropColumn("dbo.Notifications", "IdGradeAreaUserGeneralSelected");
            DropColumn("dbo.Notifications", "NotificationName");
            DropColumn("dbo.People", "UserId_Id");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Homework");
            DropTable("dbo.NotificationTypes");
        }
    }
}
