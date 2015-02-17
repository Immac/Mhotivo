namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class siwady2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicYears", "Year", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcademicYears", "Year", c => c.Int(nullable: false));
        }
    }
}
