namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResetPassewordCode", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ResetPassewordCode");
        }
    }
}
