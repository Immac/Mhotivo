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
            CreateIndex("dbo.AcademicYears", "Teacher_Id");
            CreateIndex("dbo.AcademicYears", "Course_Id");
            AddForeignKey("dbo.AcademicYears", "Teacher_Id", "dbo.People", "Id");
            AddForeignKey("dbo.AcademicYears", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
