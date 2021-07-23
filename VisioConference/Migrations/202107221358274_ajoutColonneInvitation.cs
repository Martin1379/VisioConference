namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutColonneInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "invitation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "invitation");
        }
    }
}
