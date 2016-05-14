namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_MainImage_Mapping : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId1" });
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId" });
            DropForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products");
            DropColumn("dbo.ProductImages", "Product_ProductId");
            RenameColumn(table: "dbo.ProductImages", name: "Product_ProductId1", newName: "Product_ProductId");
            CreateIndex("dbo.ProductImages", "Product_ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.ProductImages", name: "Product_ProductId", newName: "Product_ProductId1");
            AddColumn("dbo.ProductImages", "Product_ProductId", c => c.Int());
            AddForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products");
            CreateIndex("dbo.ProductImages", "Product_ProductId1");
            CreateIndex("dbo.ProductImages", "Product_ProductId");
        }
    }
}
