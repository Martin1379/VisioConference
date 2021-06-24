namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifconv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "user_id", "dbo.Users");
            DropIndex("dbo.Conversations", new[] { "user_id" });
            DropColumn("dbo.Conversations", "userID");
            RenameColumn(table: "dbo.Conversations", name: "user_id", newName: "userID");
            AlterColumn("dbo.Conversations", "userID", c => c.Int(nullable: false));
            AlterColumn("dbo.Conversations", "userID", c => c.Int(nullable: false));
            CreateIndex("dbo.Conversations", "userID");
            AddForeignKey("dbo.Conversations", "userID", "dbo.Users", "id", cascadeDelete: true);
            DropColumn("dbo.Conversations", "userFriendID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "userFriendID", c => c.String(nullable: false, maxLength: 255, unicode: false));
            DropForeignKey("dbo.Conversations", "userID", "dbo.Users");
            DropIndex("dbo.Conversations", new[] { "userID" });
            AlterColumn("dbo.Conversations", "userID", c => c.Int());
            AlterColumn("dbo.Conversations", "userID", c => c.String(nullable: false, maxLength: 255, unicode: false));
            RenameColumn(table: "dbo.Conversations", name: "userID", newName: "user_id");
            AddColumn("dbo.Conversations", "userID", c => c.String(nullable: false, maxLength: 255, unicode: false));
            CreateIndex("dbo.Conversations", "user_id");
            AddForeignKey("dbo.Conversations", "user_id", "dbo.Users", "id");
        }
    }
}
