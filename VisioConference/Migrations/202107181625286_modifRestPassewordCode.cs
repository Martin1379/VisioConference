namespace VisioConference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifRestPassewordCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ResetPassewordCode", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ResetPassewordCode", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
