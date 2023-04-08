namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseapproval1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseApprovals", "NameEn", c => c.String());
            DropColumn("dbo.CourseApprovals", "DescriptionAr");
            DropColumn("dbo.CourseApprovals", "DescriptionEn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseApprovals", "DescriptionEn", c => c.String());
            AddColumn("dbo.CourseApprovals", "DescriptionAr", c => c.String());
            AlterColumn("dbo.CourseApprovals", "NameEn", c => c.String(nullable: false));
        }
    }
}
