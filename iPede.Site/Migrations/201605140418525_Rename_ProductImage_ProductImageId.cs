namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_ProductImage_ProductImageId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "MainImageId", "dbo.ProductImages");
            DropPrimaryKey("dbo.ProductImages");
            RenameColumn("dbo.ProductImages", "ProductImageId", "Id");
            AddPrimaryKey("dbo.ProductImages", "Id");
            AddForeignKey("dbo.Products", "MainImageId", "dbo.ProductImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MainImageId", "dbo.ProductImages");
            DropPrimaryKey("dbo.ProductImages");
            RenameColumn("dbo.ProductImages", "Id", "ProductImageId");
            AddPrimaryKey("dbo.ProductImages", "ProductImageId");
            AddForeignKey("dbo.Products", "MainImageId", "dbo.ProductImages", "ProductImageId");
        }
    }
}
