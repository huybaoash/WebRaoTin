namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb26_11_2021 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BatDongSans", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.BatDongSans", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.TinTucs", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.TinTucs", "Contract", c => c.String(nullable: false));
            AlterColumn("dbo.DichVus", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.DichVus", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.ViecLams", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ViecLams", "Require", c => c.String(nullable: false));
            AlterColumn("dbo.ViecLams", "Benefit", c => c.String(nullable: false));
            AlterColumn("dbo.ViecLams", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.SanPhams", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.SanPhams", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SanPhams", "Location", c => c.String());
            AlterColumn("dbo.SanPhams", "Description", c => c.String());
            AlterColumn("dbo.ViecLams", "Location", c => c.String());
            AlterColumn("dbo.ViecLams", "Benefit", c => c.String());
            AlterColumn("dbo.ViecLams", "Require", c => c.String());
            AlterColumn("dbo.ViecLams", "Description", c => c.String());
            AlterColumn("dbo.DichVus", "Location", c => c.String());
            AlterColumn("dbo.DichVus", "Description", c => c.String());
            AlterColumn("dbo.TinTucs", "Contract", c => c.String());
            AlterColumn("dbo.TinTucs", "Title", c => c.String());
            AlterColumn("dbo.BatDongSans", "Location", c => c.String());
            AlterColumn("dbo.BatDongSans", "Description", c => c.String());
        }
    }
}
