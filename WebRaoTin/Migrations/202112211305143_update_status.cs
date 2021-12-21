namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThongBaos", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThongBaos", "Status");
        }
    }
}
