namespace Isofrio.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannerItem_VerticalPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BannerItems", "VerticalPosition", c => c.Int(nullable: false, 
                defaultValue: (int)Models.Entities.BannerContentVerticalPosition.Middle));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BannerItems", "VerticalPosition");
        }
    }
}
