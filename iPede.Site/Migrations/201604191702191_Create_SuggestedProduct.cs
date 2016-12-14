namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_SuggestedProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuggestedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderingNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuggestedProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.SuggestedProducts", new[] { "ProductId" });
            DropTable("dbo.SuggestedProducts");
        }
    }
}
