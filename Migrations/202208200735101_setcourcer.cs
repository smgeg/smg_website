namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcourcer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseTypes", "CreatedBy", c => c.String());
            AlterColumn("dbo.CourseTypes", "UpdatedBy", c => c.String());
            AlterColumn("dbo.TrainingTypes", "CreatedBy", c => c.String());
            AlterColumn("dbo.TrainingTypes", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainingTypes", "UpdatedBy", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TrainingTypes", "CreatedBy", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CourseTypes", "UpdatedBy", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CourseTypes", "CreatedBy", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
