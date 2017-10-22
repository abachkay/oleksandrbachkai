namespace oleksandrbachkai.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.PageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pages");
        }
    }
}
