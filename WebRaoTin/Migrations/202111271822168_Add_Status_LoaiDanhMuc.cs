namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Status_LoaiDanhMuc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoaiBatDongSans", "Status", c => c.String());
            AddColumn("dbo.LoaiDichVus", "Status", c => c.String());
            AddColumn("dbo.LoaiSanPhams", "Status", c => c.String());
            AddColumn("dbo.LoaiViecLams", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoaiViecLams", "Status");
            DropColumn("dbo.LoaiSanPhams", "Status");
            DropColumn("dbo.LoaiDichVus", "Status");
            DropColumn("dbo.LoaiBatDongSans", "Status");
        }
    }
}
