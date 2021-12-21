namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_ThongBao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongBaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublishDay = c.DateTime(nullable: false),
                        Description = c.String(),
                        Link = c.String(),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThongBaos", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.ThongBaos", new[] { "CustomerID" });
            DropTable("dbo.ThongBaos");
        }
    }
}
