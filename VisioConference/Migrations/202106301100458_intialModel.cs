namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        convID = c.Int(nullable: false, identity: true),
                        userID = c.Int(nullable: false),
                        userFriendID = c.Int(nullable: false),
                        message = c.String(),
                    })
                .PrimaryKey(t => t.convID)
                .ForeignKey("dbo.Users", t => t.userID, cascadeDelete: true)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pseudo = c.String(nullable: false, maxLength: 255, unicode: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Photo = c.String(),
                        Connected = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Pseudo, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "userID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Pseudo" });
            DropIndex("dbo.Conversations", new[] { "userID" });
            DropTable("dbo.Users");
            DropTable("dbo.Conversations");
        }
    }
}
