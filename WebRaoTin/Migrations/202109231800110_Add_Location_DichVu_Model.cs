namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Location_DichVu_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DichVus", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DichVus", "Location");
        }
    }
}
