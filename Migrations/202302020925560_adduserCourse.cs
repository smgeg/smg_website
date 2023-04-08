namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCourseCollections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserCourseCollections");
        }
    }
}
