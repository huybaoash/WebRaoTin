namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_PhieuXetUngTuyen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietPhieuXetTuyens", "PhieuXetUngTuyenId", "dbo.PhieuXetUngTuyens");
            DropForeignKey("dbo.ChiTietPhieuXetTuyens", "ViecLamId", "dbo.ViecLams");
            DropIndex("dbo.ChiTietPhieuXetTuyens", new[] { "PhieuXetUngTuyenId" });
            DropIndex("dbo.ChiTietPhieuXetTuyens", new[] { "ViecLamId" });
            AddColumn("dbo.PhieuXetUngTuyens", "Description", c => c.String());
            AddColumn("dbo.PhieuXetUngTuyens", "ViecLamId", c => c.Int(nullable: false));
            CreateIndex("dbo.PhieuXetUngTuyens", "ViecLamId");
            AddForeignKey("dbo.PhieuXetUngTuyens", "ViecLamId", "dbo.ViecLams", "Id", cascadeDelete: true);
            DropColumn("dbo.PhieuXetUngTuyens", "Title");
            DropTable("dbo.ChiTietPhieuXetTuyens");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChiTietPhieuXetTuyens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhieuXetUngTuyenId = c.Int(nullable: false),
                        ViecLamId = c.Int(nullable: false),
                        AboutYou = c.String(),
                        Education = c.String(),
                        Experience = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PhieuXetUngTuyens", "Title", c => c.String());
            DropForeignKey("dbo.PhieuXetUngTuyens", "ViecLamId", "dbo.ViecLams");
            DropIndex("dbo.PhieuXetUngTuyens", new[] { "ViecLamId" });
            DropColumn("dbo.PhieuXetUngTuyens", "ViecLamId");
            DropColumn("dbo.PhieuXetUngTuyens", "Description");
            CreateIndex("dbo.ChiTietPhieuXetTuyens", "ViecLamId");
            CreateIndex("dbo.ChiTietPhieuXetTuyens", "PhieuXetUngTuyenId");
            AddForeignKey("dbo.ChiTietPhieuXetTuyens", "ViecLamId", "dbo.ViecLams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietPhieuXetTuyens", "PhieuXetUngTuyenId", "dbo.PhieuXetUngTuyens", "Id", cascadeDelete: true);
        }
    }
}
