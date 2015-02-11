namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Disable", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.People", "Photo", c => c.Binary(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
