namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_benefit_attributes_on_VLTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ViecLams", "Benefit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ViecLams", "Benefit");
        }
    }
}
