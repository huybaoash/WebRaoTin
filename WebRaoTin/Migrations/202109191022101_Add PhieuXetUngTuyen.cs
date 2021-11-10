namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhieuXetUngTuyen : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhieuXetUngTuyens", t => t.PhieuXetUngTuyenId, cascadeDelete: true)
                .ForeignKey("dbo.ViecLams", t => t.ViecLamId, cascadeDelete: true)
                .Index(t => t.PhieuXetUngTuyenId)
                .Index(t => t.ViecLamId);
            
            CreateTable(
                "dbo.PhieuXetUngTuyens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublishDay = c.DateTime(nullable: false),
                        Status = c.String(),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            AddColumn("dbo.TinTucs", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietPhieuXetTuyens", "ViecLamId", "dbo.ViecLams");
            DropForeignKey("dbo.ChiTietPhieuXetTuyens", "PhieuXetUngTuyenId", "dbo.PhieuXetUngTuyens");
            DropForeignKey("dbo.PhieuXetUngTuyens", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.PhieuXetUngTuyens", new[] { "CustomerID" });
            DropIndex("dbo.ChiTietPhieuXetTuyens", new[] { "ViecLamId" });
            DropIndex("dbo.ChiTietPhieuXetTuyens", new[] { "PhieuXetUngTuyenId" });
            DropColumn("dbo.TinTucs", "Status");
            DropTable("dbo.PhieuXetUngTuyens");
            DropTable("dbo.ChiTietPhieuXetTuyens");
        }
    }
}
