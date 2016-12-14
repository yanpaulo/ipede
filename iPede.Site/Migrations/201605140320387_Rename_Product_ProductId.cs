namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Product_ProductId : DbMigration
    {
        public override void Up()
        {
            
            DropForeignKey("dbo.ProductImages", "FK_dbo.ProductImages_dbo.Products_Product_ProductId1");
            DropForeignKey("dbo.ProductImages", "FK_dbo.ProductImages_dbo.Products_ProductId");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SuggestedProducts", "ProductId", "dbo.Products");
            DropPrimaryKey("dbo.Products");
            RenameColumn("dbo.Products", "ProductId", "Id");
            AddPrimaryKey("dbo.Products", "Id");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SuggestedProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuggestedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropPrimaryKey("dbo.Products");
            RenameColumn("dbo.Products", "Id", "ProductId");
            AddPrimaryKey("dbo.Products", "ProductId");
            AddForeignKey("dbo.SuggestedProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
