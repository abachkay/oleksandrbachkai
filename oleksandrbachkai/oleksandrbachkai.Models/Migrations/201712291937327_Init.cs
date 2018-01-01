namespace oleksandrbachkai.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pages", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Pages", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pages", "Content", c => c.String());
            AlterColumn("dbo.PaUpges", "Name", c => c.String());
        }
    }
}
