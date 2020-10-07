namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDataICollectionMenuItemsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.MenuItem", "Order_OrderId");
            AddForeignKey("dbo.MenuItem", "Order_OrderId", "dbo.Order", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItem", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.MenuItem", new[] { "Order_OrderId" });
            DropColumn("dbo.MenuItem", "Order_OrderId");
        }
    }
}
