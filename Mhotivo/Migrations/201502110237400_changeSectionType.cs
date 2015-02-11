namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSectionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicYears", "Section", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicYears", "Section");
        }
    }
}
