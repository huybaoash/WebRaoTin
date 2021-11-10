namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Canloi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TinTucs", "ContractPhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TinTucs", "ContractPhoneNumber", c => c.String(nullable: false));
        }
    }
}
