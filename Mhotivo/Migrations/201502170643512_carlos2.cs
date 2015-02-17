namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carlos2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Homework", "AcademicYearDetail_Id", "dbo.AcademicYearDetails");
            DropIndex("dbo.Homework", new[] { "AcademicYearDetail_Id" });
            AddColumn("dbo.AcademicYearDetails", "TeacherStartDate", c => c.DateTime());
            AddColumn("dbo.AcademicYearDetails", "TeacherEndDate", c => c.DateTime());
            AddColumn("dbo.People", "JustARandomColumn", c => c.String());
            DropTable("dbo.Homework");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.People", "JustARandomColumn");
            DropColumn("dbo.AcademicYearDetails", "TeacherEndDate");
            DropColumn("dbo.AcademicYearDetails", "TeacherStartDate");
            CreateIndex("dbo.Homework", "AcademicYearDetail_Id");
            AddForeignKey("dbo.Homework", "AcademicYearDetail_Id", "dbo.AcademicYearDetails", "Id");
        }
    }
}
