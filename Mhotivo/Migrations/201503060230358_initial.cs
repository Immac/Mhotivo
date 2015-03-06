namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            CreateTable(
                "dbo.UserRols",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Role_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.AcademicYearDetails", "TeacherStartDate", c => c.DateTime());
            AddColumn("dbo.AcademicYearDetails", "TeacherEndDate", c => c.DateTime());
            AddColumn("dbo.People", "User_Id", c => c.Int());
            CreateIndex("dbo.People", "User_Id");
            AddForeignKey("dbo.People", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "Role_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            DropForeignKey("dbo.UserRols", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRols", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.People", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRols", new[] { "User_Id" });
            DropIndex("dbo.UserRols", new[] { "Role_Id" });
            DropIndex("dbo.People", new[] { "User_Id" });
            DropColumn("dbo.People", "User_Id");
            DropColumn("dbo.AcademicYearDetails", "TeacherEndDate");
            DropColumn("dbo.AcademicYearDetails", "TeacherStartDate");
            DropTable("dbo.UserRols");
            CreateIndex("dbo.Users", "Role_Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
        }
    }
}
