namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changementConnected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Etat", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Connected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Connected", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Etat");
        }
    }
}
