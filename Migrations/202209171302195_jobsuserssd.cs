namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobsuserssd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobUsers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.JobUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.JobUsers", "Mobile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobUsers", "Mobile", c => c.String());
            AlterColumn("dbo.JobUsers", "Email", c => c.String());
            AlterColumn("dbo.JobUsers", "Name", c => c.String());
        }
    }
}
