namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasdoo5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AgricultureNetworkUsers", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.AgricultureNetworkUsers", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AgricultureNetworkUsers", "Password", c => c.String());
            AlterColumn("dbo.AgricultureNetworkUsers", "UserName", c => c.String());
        }
    }
}
