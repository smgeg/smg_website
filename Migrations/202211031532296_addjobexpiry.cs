namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobexpiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "ExpirationDate");
        }
    }
}
