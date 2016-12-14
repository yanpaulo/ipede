namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_Image_Mapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId" });
            DropColumn("dbo.ProductImages", "ProductId");
            RenameColumn(table: "dbo.ProductImages", name: "Product_ProductId", newName: "ProductId");
            AlterColumn("dbo.ProductImages", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductImages", "ProductId");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            AlterColumn("dbo.ProductImages", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.ProductImages", name: "ProductId", newName: "Product_ProductId");
            AddColumn("dbo.ProductImages", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductImages", "Product_ProductId");
            AddForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
