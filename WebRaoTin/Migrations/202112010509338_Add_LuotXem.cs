namespace WebRaoTin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LuotXem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTucs", "LuotXem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TinTucs", "LuotXem");
        }
    }
}
