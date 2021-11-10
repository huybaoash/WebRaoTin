namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Config_BinhLuan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BinhLuans", "PublishDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.BinhLuans", "TinTucId", c => c.Int(nullable: false));
            CreateIndex("dbo.BinhLuans", "TinTucId");
            AddForeignKey("dbo.BinhLuans", "TinTucId", "dbo.TinTucs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BinhLuans", "TinTucId", "dbo.TinTucs");
            DropIndex("dbo.BinhLuans", new[] { "TinTucId" });
            DropColumn("dbo.BinhLuans", "TinTucId");
            DropColumn("dbo.BinhLuans", "PublishDay");
        }
    }
}
