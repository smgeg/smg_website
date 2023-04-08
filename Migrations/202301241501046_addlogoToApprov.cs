namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlogoToApprov : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseApprovals", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseApprovals", "Image");
        }
    }
}
