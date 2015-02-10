namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppointmentDiaries", "AppointmentParticipants_Id", "dbo.AppointmentParticipants");
            DropIndex("dbo.AppointmentDiaries", new[] { "AppointmentParticipants_Id" });
            //AddColumn("dbo.People", "Photo", c => c.Binary());
            //AddColumn("dbo.People", "Disable", c => c.Boolean(nullable: false));
            DropColumn("dbo.AppointmentDiaries", "AppointmentParticipants_Id");
            DropTable("dbo.AppointmentParticipants");
        }
        
        public override void Down()
        {
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
            //DropColumn("dbo.People", "Disable");
            //DropColumn("dbo.People", "Photo");
            CreateIndex("dbo.AppointmentDiaries", "AppointmentParticipants_Id");
            AddForeignKey("dbo.AppointmentDiaries", "AppointmentParticipants_Id", "dbo.AppointmentParticipants", "Id");
        }
    }
}
