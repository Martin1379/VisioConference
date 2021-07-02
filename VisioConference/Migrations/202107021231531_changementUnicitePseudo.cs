namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changementUnicitePseudo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Pseudo" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "Pseudo", unique: true);
        }
    }
}
