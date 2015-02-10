namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameActived : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Disable", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "Activate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Activate", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "Disable");
        }
    }
}
