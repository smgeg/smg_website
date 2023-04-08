namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcoursdjh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "DetailsAr", c => c.String());
            AlterColumn("dbo.Courses", "DetailsEn", c => c.String());
            AlterColumn("dbo.Courses", "TargetAr", c => c.String());
            AlterColumn("dbo.Courses", "TargetEn", c => c.String());
            AlterColumn("dbo.Courses", "ContentDetailsAr", c => c.String());
            AlterColumn("dbo.Courses", "ContentDetailsEn", c => c.String());
            AlterColumn("dbo.Courses", "CoursePersonsAr", c => c.String());
            AlterColumn("dbo.Courses", "CoursePersonsEn", c => c.String());
            AlterColumn("dbo.Trainings", "DetailsAr", c => c.String());
            AlterColumn("dbo.Trainings", "DetailsEn", c => c.String());
            AlterColumn("dbo.Trainings", "TargetAr", c => c.String());
            AlterColumn("dbo.Trainings", "TargetEn", c => c.String());
            AlterColumn("dbo.Trainings", "ContentDetailsAr", c => c.String());
            AlterColumn("dbo.Trainings", "ContentDetailsEn", c => c.String());
            AlterColumn("dbo.Trainings", "CoursePersonsAr", c => c.String());
            AlterColumn("dbo.Trainings", "CoursePersonsEn", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainings", "CoursePersonsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "CoursePersonsAr", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "ContentDetailsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "ContentDetailsAr", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "TargetEn", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "TargetAr", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "DetailsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Trainings", "DetailsAr", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "CoursePersonsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "CoursePersonsAr", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "ContentDetailsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "ContentDetailsAr", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "TargetEn", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "TargetAr", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "DetailsEn", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "DetailsAr", c => c.String(nullable: false));
        }
    }
}
