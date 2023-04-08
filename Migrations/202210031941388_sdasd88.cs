namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasd88 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgricultureNetworkUsers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AgricultureNetworkUsers", "Email");
        }
    }
}
