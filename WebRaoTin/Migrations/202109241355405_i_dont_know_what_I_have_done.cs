namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class i_dont_know_what_I_have_done : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DateBorn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateBorn", c => c.DateTime(nullable: false));
        }
    }
}
