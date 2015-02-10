namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameActived : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "Activate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Activate", c => c.Boolean(nullable: false));
        }
    }
}
