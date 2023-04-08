namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasd45 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgricultureNetworkUsers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AgricultureNetworkUsers", "IsActive");
        }
    }
}
