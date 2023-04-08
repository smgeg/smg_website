namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcourcerصي : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseTypes", "DescriptionAr", c => c.String());
            AddColumn("dbo.CourseTypes", "DescriptionEn", c => c.String());
            AddColumn("dbo.TrainingTypes", "DescriptionAr", c => c.String());
            AddColumn("dbo.TrainingTypes", "DescriptionEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainingTypes", "DescriptionEn");
            DropColumn("dbo.TrainingTypes", "DescriptionAr");
            DropColumn("dbo.CourseTypes", "DescriptionEn");
            DropColumn("dbo.CourseTypes", "DescriptionAr");
        }
    }
}
