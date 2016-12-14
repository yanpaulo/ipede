namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Category_CategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            RenameColumn(table: "dbo.Categories", name: "CategoryId", newName: "Id");
            AddPrimaryKey("dbo.Categories", "Id");
            AddForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            RenameColumn(table: "dbo.Categories", name: "Id", newName: "CategoryId");
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
