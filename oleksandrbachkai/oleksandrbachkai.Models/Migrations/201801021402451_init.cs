namespace oleksandrbachkai.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pages", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pages", "Content", c => c.String(nullable: false));
        }
    }
}
