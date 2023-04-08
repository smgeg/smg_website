namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcour2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Image", c => c.String(nullable: false));
        }
    }
}
