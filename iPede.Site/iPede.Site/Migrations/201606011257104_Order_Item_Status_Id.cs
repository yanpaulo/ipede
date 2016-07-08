namespace iPede.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Item_Status_Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            RenameColumn(table: "dbo.OrderItems", name: "Order_OrderId", newName: "Order_Id");
            RenameIndex(table: "dbo.OrderItems", name: "IX_Order_OrderId", newName: "IX_Order_Id");
            DropPrimaryKey("dbo.OrderItems");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.OrderStatus");
            RenameColumn("dbo.OrderItems", "OrderItemId", "Id");
            RenameColumn("dbo.Orders", "OrderId", "Id");
            RenameColumn("dbo.OrderStatus", "OrderStatusId", "Id");
            AddPrimaryKey("dbo.OrderItems", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.OrderStatus", "Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropPrimaryKey("dbo.OrderStatus");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.OrderItems");
            RenameColumn("dbo.OrderItems", "Id", "OrderItemId");
            RenameColumn("dbo.Orders", "Id", "OrderId");
            RenameColumn("dbo.OrderStatus", "Id", "OrderStatusId");
            AddPrimaryKey("dbo.OrderStatus", "OrderStatusId");
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.OrderItems", "OrderItemId");
            RenameIndex(table: "dbo.OrderItems", name: "IX_Order_Id", newName: "IX_Order_OrderId");
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "Order_OrderId");
            AddForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus", "OrderStatusId", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
