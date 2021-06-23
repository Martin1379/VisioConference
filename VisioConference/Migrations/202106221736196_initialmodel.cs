namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        convID = c.Int(nullable: false, identity: true),
                        userID = c.String(nullable: false, maxLength: 255, unicode: false),
                        userFriendID = c.String(nullable: false, maxLength: 255, unicode: false),
                        message = c.String(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.convID)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 255, unicode: false),
                        connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.login, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "user_id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "login" });
            DropIndex("dbo.Conversations", new[] { "user_id" });
            DropTable("dbo.Users");
            DropTable("dbo.Conversations");
        }
    }
}
