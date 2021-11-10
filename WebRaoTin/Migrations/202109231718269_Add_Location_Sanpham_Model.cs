namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Location_Sanpham_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "Location");
        }
    }
}
