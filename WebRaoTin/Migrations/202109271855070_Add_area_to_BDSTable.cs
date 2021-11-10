namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_area_to_BDSTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatDongSans", "Area", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatDongSans", "Area");
        }
    }
}
