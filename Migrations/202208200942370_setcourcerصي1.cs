namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcourcerصي1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseTypes", "NameAr", c => c.String(nullable: false));
            AlterColumn("dbo.CourseTypes", "NameEn", c => c.String(nullable: false));
            AlterColumn("dbo.TrainingTypes", "NameAr", c => c.String(nullable: false));
            AlterColumn("dbo.TrainingTypes", "NameEn", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainingTypes", "NameEn", c => c.String());
            AlterColumn("dbo.TrainingTypes", "NameAr", c => c.String());
            AlterColumn("dbo.CourseTypes", "NameEn", c => c.String());
            AlterColumn("dbo.CourseTypes", "NameAr", c => c.String());
        }
    }
}
