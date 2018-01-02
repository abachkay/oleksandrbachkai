namespace oleksandrbachkai.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pages");
        }
    }
}
