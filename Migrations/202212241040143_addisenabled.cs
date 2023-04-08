namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisenabled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourseTypes", "IsEnable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseTypes", "IsEnable");
            DropColumn("dbo.Courses", "IsEnable");
        }
    }
}
