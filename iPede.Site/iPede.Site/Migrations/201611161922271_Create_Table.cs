namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM dbo.OrderItems");
            Sql("DELETE FROM dbo.Orders");

            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "TableId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "TableId");
            AddForeignKey("dbo.Orders", "TableId", "dbo.Tables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TableId", "dbo.Tables");
            DropIndex("dbo.Orders", new[] { "TableId" });
            DropColumn("dbo.Orders", "TableId");
            DropTable("dbo.Tables");
        }
    }
}
