namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddate_join : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateJoin", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateJoin");
        }
    }
}
