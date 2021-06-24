namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifconv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "userFriendID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "userFriendID");
        }
    }
}
