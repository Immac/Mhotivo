namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Notifications", "NotificationCreator_Id", "dbo.Users");
            //DropForeignKey("dbo.Notifications", "NotificationType_NotificationTypeId", "dbo.NotificationTypes");
            //DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            //DropForeignKey("dbo.UserNotifications", "UserId", "dbo.Users");
            //DropForeignKey("dbo.People", "UserId_Id", "dbo.Users");
            //DropIndex("dbo.Notifications", new[] { "NotificationCreator_Id" });
            //DropIndex("dbo.Notifications", new[] { "NotificationType_NotificationTypeId" });
            //DropIndex("dbo.People", new[] { "UserId_Id" });
            //DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            //DropIndex("dbo.UserNotifications", new[] { "UserId" });
            //RenameColumn(table: "dbo.People", name: "StartDate", newName: "__mig_tmp__0");
            //RenameColumn(table: "dbo.People", name: "Biography", newName: "__mig_tmp__1");
            //RenameColumn(table: "dbo.People", name: "Biography1", newName: "Biography");
            //RenameColumn(table: "dbo.People", name: "StartDate1", newName: "StartDate");
            //RenameColumn(table: "dbo.People", name: "__mig_tmp__0", newName: "StartDate1");
            //RenameColumn(table: "dbo.People", name: "__mig_tmp__1", newName: "Biography1");
            //CreateTable(
            //    "dbo.AcademicYearDetails",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TeacherStartDate = c.DateTime(),
            //            TeacherEndDate = c.DateTime(),
            //            Schedule = c.DateTime(),
            //            Room = c.String(),
            //            AcademicYear_Id = c.Int(),
            //            Course_Id = c.Int(),
            //            Teacher_Id = c.Long(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AcademicYears", t => t.AcademicYear_Id)
            //    .ForeignKey("dbo.Courses", t => t.Course_Id)
            //    .ForeignKey("dbo.People", t => t.Teacher_Id)
            //    .Index(t => t.AcademicYear_Id)
            //    .Index(t => t.Course_Id)
            //    .Index(t => t.Teacher_Id);
            
            //AddColumn("dbo.AcademicYears", "Section", c => c.String());
            //AddColumn("dbo.Notifications", "EventName", c => c.String());
            //AddColumn("dbo.Notifications", "From", c => c.String());
            //AddColumn("dbo.Notifications", "To", c => c.String());
            //AddColumn("dbo.Notifications", "WithCopyTo", c => c.String());
            //AddColumn("dbo.Notifications", "WithHiddenCopyTo", c => c.String());
            //AddColumn("dbo.Notifications", "Subject", c => c.String());
            //DropColumn("dbo.Notifications", "NotificationName");
            //DropColumn("dbo.Notifications", "IdGradeAreaUserGeneralSelected");
            //DropColumn("dbo.Notifications", "SendingEmail");
            //DropColumn("dbo.Notifications", "NotificationCreator_Id");
            //DropColumn("dbo.Notifications", "NotificationType_NotificationTypeId");
            //DropColumn("dbo.People", "UserId_Id");
            //DropTable("dbo.NotificationTypes");
            //DropTable("dbo.UserNotifications");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.UserNotifications",
            //    c => new
            //        {
            //            NotificationId = c.Long(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.NotificationId, t.UserId });
            
            //CreateTable(
            //    "dbo.NotificationTypes",
            //    c => new
            //        {
            //            NotificationTypeId = c.Int(nullable: false, identity: true),
            //            TypeDescription = c.String(),
            //        })
            //    .PrimaryKey(t => t.NotificationTypeId);
            
            //AddColumn("dbo.People", "UserId_Id", c => c.Int());
            //AddColumn("dbo.Notifications", "NotificationType_NotificationTypeId", c => c.Int());
            //AddColumn("dbo.Notifications", "NotificationCreator_Id", c => c.Int());
            //AddColumn("dbo.Notifications", "SendingEmail", c => c.Boolean(nullable: false));
            //AddColumn("dbo.Notifications", "IdGradeAreaUserGeneralSelected", c => c.Int(nullable: false));
            //AddColumn("dbo.Notifications", "NotificationName", c => c.String());
            //DropForeignKey("dbo.AcademicYearDetails", "Teacher_Id", "dbo.People");
            //DropForeignKey("dbo.AcademicYearDetails", "Course_Id", "dbo.Courses");
            //DropForeignKey("dbo.AcademicYearDetails", "AcademicYear_Id", "dbo.AcademicYears");
            //DropIndex("dbo.AcademicYearDetails", new[] { "Teacher_Id" });
            //DropIndex("dbo.AcademicYearDetails", new[] { "Course_Id" });
            //DropIndex("dbo.AcademicYearDetails", new[] { "AcademicYear_Id" });
            //DropColumn("dbo.Notifications", "Subject");
            //DropColumn("dbo.Notifications", "WithHiddenCopyTo");
            //DropColumn("dbo.Notifications", "WithCopyTo");
            //DropColumn("dbo.Notifications", "To");
            //DropColumn("dbo.Notifications", "From");
            //DropColumn("dbo.Notifications", "EventName");
            //DropColumn("dbo.AcademicYears", "Section");
            //DropTable("dbo.AcademicYearDetails");
            //RenameColumn(table: "dbo.People", name: "Biography1", newName: "__mig_tmp__1");
            //RenameColumn(table: "dbo.People", name: "StartDate1", newName: "__mig_tmp__0");
            //RenameColumn(table: "dbo.People", name: "StartDate", newName: "StartDate1");
            //RenameColumn(table: "dbo.People", name: "Biography", newName: "Biography1");
            //RenameColumn(table: "dbo.People", name: "__mig_tmp__1", newName: "Biography");
            //RenameColumn(table: "dbo.People", name: "__mig_tmp__0", newName: "StartDate");
            //CreateIndex("dbo.UserNotifications", "UserId");
            //CreateIndex("dbo.UserNotifications", "NotificationId");
            //CreateIndex("dbo.People", "UserId_Id");
            //CreateIndex("dbo.Notifications", "NotificationType_NotificationTypeId");
            //CreateIndex("dbo.Notifications", "NotificationCreator_Id");
            //AddForeignKey("dbo.People", "UserId_Id", "dbo.Users", "Id");
            //AddForeignKey("dbo.UserNotifications", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Notifications", "NotificationType_NotificationTypeId", "dbo.NotificationTypes", "NotificationTypeId");
            //AddForeignKey("dbo.Notifications", "NotificationCreator_Id", "dbo.Users", "Id");
        }
    }
}
