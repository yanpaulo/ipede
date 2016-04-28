namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDbGargabe : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AboutUs");
            DropTable("dbo.BannerItems");
            DropTable("dbo.Services");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
