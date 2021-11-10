namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BatDongSans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Video = c.String(),
                        Description = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinTucs", t => t.TinTucId, cascadeDelete: true)
                .Index(t => t.TinTucId);
            
            CreateTable(
                "dbo.TinTucs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublishDay = c.DateTime(nullable: false),
                        EndDay = c.DateTime(nullable: false),
                        Contract = c.String(),
                        ContractPhoneNumber = c.String(),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.BinhLuans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.DichVus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Description = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinTucs", t => t.TinTucId, cascadeDelete: true)
                .Index(t => t.TinTucId);
            
            CreateTable(
                "dbo.LoaiBatDongSans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BatDongSanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BatDongSans", t => t.BatDongSanId, cascadeDelete: true)
                .Index(t => t.BatDongSanId);
            
            CreateTable(
                "dbo.LoaiDichVus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DichVuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DichVus", t => t.DichVuId, cascadeDelete: true)
                .Index(t => t.DichVuId);
            
            CreateTable(
                "dbo.LoaiSanPhams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SanPhamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.SanPhamId);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Description = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinTucs", t => t.TinTucId, cascadeDelete: true)
                .Index(t => t.TinTucId);
            
            CreateTable(
                "dbo.LoaiTinTucs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinTucs", t => t.TinTucId, cascadeDelete: true)
                .Index(t => t.TinTucId);
            
            CreateTable(
                "dbo.ViecLams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Require = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(),
                        Image = c.String(),
                        TinTucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinTucs", t => t.TinTucId, cascadeDelete: true)
                .Index(t => t.TinTucId);
            
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAdress", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateBorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConfirmPublicInfo", c => c.String());
            AddColumn("dbo.AspNetUsers", "CMND", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViecLams", "TinTucId", "dbo.TinTucs");
            DropForeignKey("dbo.LoaiTinTucs", "TinTucId", "dbo.TinTucs");
            DropForeignKey("dbo.LoaiSanPhams", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "TinTucId", "dbo.TinTucs");
            DropForeignKey("dbo.LoaiDichVus", "DichVuId", "dbo.DichVus");
            DropForeignKey("dbo.LoaiBatDongSans", "BatDongSanId", "dbo.BatDongSans");
            DropForeignKey("dbo.DichVus", "TinTucId", "dbo.TinTucs");
            DropForeignKey("dbo.BinhLuans", "CustomerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BatDongSans", "TinTucId", "dbo.TinTucs");
            DropForeignKey("dbo.TinTucs", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.ViecLams", new[] { "TinTucId" });
            DropIndex("dbo.LoaiTinTucs", new[] { "TinTucId" });
            DropIndex("dbo.SanPhams", new[] { "TinTucId" });
            DropIndex("dbo.LoaiSanPhams", new[] { "SanPhamId" });
            DropIndex("dbo.LoaiDichVus", new[] { "DichVuId" });
            DropIndex("dbo.LoaiBatDongSans", new[] { "BatDongSanId" });
            DropIndex("dbo.DichVus", new[] { "TinTucId" });
            DropIndex("dbo.BinhLuans", new[] { "CustomerID" });
            DropIndex("dbo.TinTucs", new[] { "CustomerID" });
            DropIndex("dbo.BatDongSans", new[] { "TinTucId" });
            DropColumn("dbo.AspNetUsers", "CMND");
            DropColumn("dbo.AspNetUsers", "ConfirmPublicInfo");
            DropColumn("dbo.AspNetUsers", "DateBorn");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "HomeAdress");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "Role");
            DropTable("dbo.ViecLams");
            DropTable("dbo.LoaiTinTucs");
            DropTable("dbo.SanPhams");
            DropTable("dbo.LoaiSanPhams");
            DropTable("dbo.LoaiDichVus");
            DropTable("dbo.LoaiBatDongSans");
            DropTable("dbo.DichVus");
            DropTable("dbo.BinhLuans");
            DropTable("dbo.TinTucs");
            DropTable("dbo.BatDongSans");
        }
    }
}
