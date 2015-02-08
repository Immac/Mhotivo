namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcademicYearDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AcademicYears", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.AcademicYears", "Teacher_Id", "dbo.People");
            DropIndex("dbo.AcademicYears", new[] { "Course_Id" });
            DropIndex("dbo.AcademicYears", new[] { "Teacher_Id" });
            CreateTable(
                "dbo.AcademicYearDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherStartDate = c.DateTime(),
                        TeacherEndDate = c.DateTime(),
                        Schedule = c.DateTime(),
                        Room = c.String(),
                        AcademicYear_Id = c.Int(),
                        Course_Id = c.Int(),
                        Teacher_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYear_Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.People", t => t.Teacher_Id)
                .Index(t => t.AcademicYear_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.AppointmentParticipants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FKUserGroup = c.Long(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AppointmentDiaries", "AppointmentParticipants_Id", c => c.Int());
            CreateIndex("dbo.AppointmentDiaries", "AppointmentParticipants_Id");
            AddForeignKey("dbo.AppointmentDiaries", "AppointmentParticipants_Id", "dbo.AppointmentParticipants", "Id");
            DropColumn("dbo.AcademicYears", "TeacherStartDate");
            DropColumn("dbo.AcademicYears", "TeacherEndDate");
            DropColumn("dbo.AcademicYears", "Schedule");
            DropColumn("dbo.AcademicYears", "Room");
            DropColumn("dbo.AcademicYears", "StudentsLimit");
            DropColumn("dbo.AcademicYears", "StudentsCount");
            DropColumn("dbo.AcademicYears", "Course_Id");
            DropColumn("dbo.AcademicYears", "Teacher_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AcademicYears", "Teacher_Id", c => c.Long());
            AddColumn("dbo.AcademicYears", "Course_Id", c => c.Int());
            AddColumn("dbo.AcademicYears", "StudentsCount", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicYears", "StudentsLimit", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicYears", "Room", c => c.String());
            AddColumn("dbo.AcademicYears", "Schedule", c => c.DateTime());
            AddColumn("dbo.AcademicYears", "TeacherEndDate", c => c.DateTime());
            AddColumn("dbo.AcademicYears", "TeacherStartDate", c => c.DateTime());
            DropForeignKey("dbo.AppointmentDiaries", "AppointmentParticipants_Id", "dbo.AppointmentParticipants");
            DropForeignKey("dbo.AcademicYearDetails", "Teacher_Id", "dbo.People");
            DropForeignKey("dbo.AcademicYearDetails", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.AcademicYearDetails", "AcademicYear_Id", "dbo.AcademicYears");
            DropIndex("dbo.AppointmentDiaries", new[] { "AppointmentParticipants_Id" });
            DropIndex("dbo.AcademicYearDetails", new[] { "Teacher_Id" });
            DropIndex("dbo.AcademicYearDetails", new[] { "Course_Id" });
            DropIndex("dbo.AcademicYearDetails", new[] { "AcademicYear_Id" });
            DropColumn("dbo.AppointmentDiaries", "AppointmentParticipants_Id");
            DropTable("dbo.AppointmentParticipants");
            DropTable("dbo.AcademicYearDetails");
            CreateIndex("dbo.AcademicYears", "Teacher_Id");
            CreateIndex("dbo.AcademicYears", "Course_Id");
            AddForeignKey("dbo.AcademicYears", "Teacher_Id", "dbo.People", "Id");
            AddForeignKey("dbo.AcademicYears", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
