namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoaiBatDongSans", "BatDongSanId", "dbo.BatDongSans");
            DropForeignKey("dbo.LoaiDichVus", "DichVuId", "dbo.DichVus");
            DropForeignKey("dbo.LoaiSanPhams", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.LoaiTinTucs", "TinTucId", "dbo.TinTucs");
            DropIndex("dbo.LoaiBatDongSans", new[] { "BatDongSanId" });
            DropIndex("dbo.LoaiDichVus", new[] { "DichVuId" });
            DropIndex("dbo.LoaiSanPhams", new[] { "SanPhamId" });
            DropIndex("dbo.LoaiTinTucs", new[] { "TinTucId" });
            AddColumn("dbo.BatDongSans", "LoaiBatDongSanId", c => c.Int(nullable: false));
            AddColumn("dbo.DichVus", "LoaiDichVuId", c => c.Int(nullable: false));
            AddColumn("dbo.SanPhams", "LoaiSanPhamId", c => c.Int(nullable: false));
            CreateIndex("dbo.BatDongSans", "LoaiBatDongSanId");
            CreateIndex("dbo.DichVus", "LoaiDichVuId");
            CreateIndex("dbo.SanPhams", "LoaiSanPhamId");
            AddForeignKey("dbo.BatDongSans", "LoaiBatDongSanId", "dbo.LoaiBatDongSans", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DichVus", "LoaiDichVuId", "dbo.LoaiDichVus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SanPhams", "LoaiSanPhamId", "dbo.LoaiSanPhams", "Id", cascadeDelete: true);
            DropColumn("dbo.LoaiBatDongSans", "BatDongSanId");
            DropColumn("dbo.LoaiDichVus", "DichVuId");
            DropColumn("dbo.LoaiSanPhams", "SanPhamId");
            DropTable("dbo.LoaiTinTucs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoaiTinTucs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LoaiSanPhams", "SanPhamId", c => c.Int(nullable: false));
            AddColumn("dbo.LoaiDichVus", "DichVuId", c => c.Int(nullable: false));
            AddColumn("dbo.LoaiBatDongSans", "BatDongSanId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SanPhams", "LoaiSanPhamId", "dbo.LoaiSanPhams");
            DropForeignKey("dbo.DichVus", "LoaiDichVuId", "dbo.LoaiDichVus");
            DropForeignKey("dbo.BatDongSans", "LoaiBatDongSanId", "dbo.LoaiBatDongSans");
            DropIndex("dbo.SanPhams", new[] { "LoaiSanPhamId" });
            DropIndex("dbo.DichVus", new[] { "LoaiDichVuId" });
            DropIndex("dbo.BatDongSans", new[] { "LoaiBatDongSanId" });
            DropColumn("dbo.SanPhams", "LoaiSanPhamId");
            DropColumn("dbo.DichVus", "LoaiDichVuId");
            DropColumn("dbo.BatDongSans", "LoaiBatDongSanId");
            CreateIndex("dbo.LoaiTinTucs", "TinTucId");
            CreateIndex("dbo.LoaiSanPhams", "SanPhamId");
            CreateIndex("dbo.LoaiDichVus", "DichVuId");
            CreateIndex("dbo.LoaiBatDongSans", "BatDongSanId");
            AddForeignKey("dbo.LoaiTinTucs", "TinTucId", "dbo.TinTucs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoaiSanPhams", "SanPhamId", "dbo.SanPhams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoaiDichVus", "DichVuId", "dbo.DichVus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoaiBatDongSans", "BatDongSanId", "dbo.BatDongSans", "Id", cascadeDelete: true);
        }
    }
}
