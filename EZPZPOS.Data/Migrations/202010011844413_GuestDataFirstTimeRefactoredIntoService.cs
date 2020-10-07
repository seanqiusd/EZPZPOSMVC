namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestDataFirstTimeRefactoredIntoService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guest", "FirstTime", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guest", "FirstTime");
        }
    }
}
