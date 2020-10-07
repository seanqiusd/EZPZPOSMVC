namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestDataLastVisitUtcRemovedToBeStretchGoal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Guest", "LastVisitUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guest", "LastVisitUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
