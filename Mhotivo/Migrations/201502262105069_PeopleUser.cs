namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeopleUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Role_Id" });
            RenameColumn(table: "dbo.Roles", name: "Role_Id", newName: "User_Id");
            AddColumn("dbo.People", "User_Id", c => c.Int());
            CreateIndex("dbo.People", "User_Id");
            CreateIndex("dbo.Roles", "User_Id");
            AddForeignKey("dbo.People", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "Role_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            DropForeignKey("dbo.People", "User_Id", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropIndex("dbo.People", new[] { "User_Id" });
            DropColumn("dbo.People", "User_Id");
            RenameColumn(table: "dbo.Roles", name: "User_Id", newName: "Role_Id");
            CreateIndex("dbo.Users", "Role_Id");
        }
    }
}
