namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbSetsAddedForGustsandOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        ServerId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactNumber = c.String(nullable: false),
                        FullAddress = c.String(),
                        Notes = c.String(),
                        LastVisitUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.GuestId)
                .ForeignKey("dbo.ApplicationUser", t => t.ServerId)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        GuestId = c.Int(nullable: false),
                        OrderDateTimeUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        TypeOfOrder = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Notes = c.String(),
                        Gratuity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Guest", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItem", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.GuestId)
                .Index(t => t.MenuItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "MenuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.Order", "GuestId", "dbo.Guest");
            DropForeignKey("dbo.Guest", "ServerId", "dbo.ApplicationUser");
            DropIndex("dbo.Order", new[] { "MenuItemId" });
            DropIndex("dbo.Order", new[] { "GuestId" });
            DropIndex("dbo.Guest", new[] { "ServerId" });
            DropTable("dbo.Order");
            DropTable("dbo.Guest");
        }
    }
}
