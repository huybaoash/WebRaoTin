namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Status_Account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.String());
            DropColumn("dbo.AspNetUsers", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.String());
            DropColumn("dbo.AspNetUsers", "Status");
        }
    }
}
