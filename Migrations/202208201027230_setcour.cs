namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseTypeId = c.Int(nullable: false),
                        NameAr = c.String(nullable: false),
                        NameEn = c.String(nullable: false),
                        DescriptionAr = c.String(),
                        DescriptionEn = c.String(),
                        Price = c.Int(nullable: false),
                        HourlyDuration = c.Int(nullable: false),
                        LecturesCount = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                        DetailsAr = c.String(nullable: false),
                        DetailsEn = c.String(nullable: false),
                        TargetAr = c.String(nullable: false),
                        TargetEn = c.String(nullable: false),
                        ContentDetailsAr = c.String(nullable: false),
                        ContentDetailsEn = c.String(nullable: false),
                        CoursePersonsAr = c.String(nullable: false),
                        CoursePersonsEn = c.String(nullable: false),
                        CourseDate = c.DateTime(nullable: false),
                        Image = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Courses");
        }
    }
}
