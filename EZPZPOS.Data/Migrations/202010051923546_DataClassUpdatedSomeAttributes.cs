namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataClassUpdatedSomeAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuItem", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItem", "Description", c => c.String(nullable: false));
        }
    }
}
