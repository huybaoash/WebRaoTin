namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_LoaiVL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoaiViecLams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ViecLams", "LoaiViecLamId", c => c.Int(nullable: false));
            CreateIndex("dbo.ViecLams", "LoaiViecLamId");
            AddForeignKey("dbo.ViecLams", "LoaiViecLamId", "dbo.LoaiViecLams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViecLams", "LoaiViecLamId", "dbo.LoaiViecLams");
            DropIndex("dbo.ViecLams", new[] { "LoaiViecLamId" });
            DropColumn("dbo.ViecLams", "LoaiViecLamId");
            DropTable("dbo.LoaiViecLams");
        }
    }
}
