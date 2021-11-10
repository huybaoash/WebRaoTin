namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Location_BatDongSan_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatDongSans", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatDongSans", "Location");
        }
    }
}
