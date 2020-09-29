namespace EZPZPOS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestDataPhoneNumberRequiredAttributeRemoved : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guest", "ContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guest", "ContactNumber", c => c.String(nullable: false));
        }
    }
}
