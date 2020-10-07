namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuItemDataAndModelsServingsInStockAndIsAvailableRefactoredIntoService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItem", "IsAvailable");
        }
    }
}
