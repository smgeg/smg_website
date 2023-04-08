namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcour1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CourseDate", c => c.DateTime());
            AlterColumn("dbo.Courses", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Image", c => c.String());
            AlterColumn("dbo.Courses", "CourseDate", c => c.DateTime(nullable: false));
        }
    }
}
