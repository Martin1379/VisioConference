namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifconv21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Conversations", "userFriendID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conversations", "userFriendID", c => c.String(nullable: false));
        }
    }
}
