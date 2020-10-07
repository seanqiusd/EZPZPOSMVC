namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedORderDataICollectionMenuItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItem", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.MenuItem", new[] { "Order_OrderId" });
            DropColumn("dbo.MenuItem", "Order_OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuItem", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.MenuItem", "Order_OrderId");
            AddForeignKey("dbo.MenuItem", "Order_OrderId", "dbo.Order", "OrderId");
        }
    }
}
