namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobexpiry2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime(nullable: false));
        }
    }
}
