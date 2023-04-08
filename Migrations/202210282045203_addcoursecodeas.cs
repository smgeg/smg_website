namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoursecodeas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgricultureNetworkUsers", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AgricultureNetworkUsers", "UserType");
        }
    }
}
