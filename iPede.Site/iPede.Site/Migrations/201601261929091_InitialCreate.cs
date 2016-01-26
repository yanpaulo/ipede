namespace Isofrio.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        AboutUsId = c.Int(nullable: false, identity: true),
                        FirstQuadrantTitle = c.String(),
                        FirstQuadrantDescription = c.String(),
                        SecondQuadrantTitle = c.String(),
                        ThirdQuadrantTitle = c.String(),
                        FourthQuadrantTitle = c.String(),
                        FourthQuadrantDescription = c.String(),
                    })
                .PrimaryKey(t => t.AboutUsId);
            
            CreateTable(
                "dbo.BannerItems",
                c => new
                    {
                        BannerItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(),
                        ImageUrl = c.String(nullable: false),
                        VerticalPosition = c.Int(nullable: false),
                        LinkUrl = c.String(),
                        LinkUrlType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BannerItemId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        ParentCategoryId = c.Int(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        MainImageId = c.Int(),
                        Name = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        FullDescription = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ProductImages", t => t.MainImageId)
                .Index(t => t.CategoryId)
                .Index(t => t.MainImageId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageId = c.Int(nullable: false, identity: true),
                        CustomDirectory = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Filename = c.String(),
                        ThumbFilename = c.String(),
                        Product_ProductId = c.Int(),
                        Product_ProductId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId1)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Product_ProductId1);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Icon = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MainImageId", "dbo.ProductImages");
            DropForeignKey("dbo.ProductImages", "Product_ProductId1", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId1" });
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId" });
            DropIndex("dbo.Products", new[] { "MainImageId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            DropTable("dbo.Services");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.BannerItems");
            DropTable("dbo.AboutUs");
        }
    }
}
