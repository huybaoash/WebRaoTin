namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNull_Salary_PriceBDS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TinTucs", "ContractPhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TinTucs", "ContractPhoneNumber", c => c.String());
        }
    }
}
