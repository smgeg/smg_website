namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseapproval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseApprovalCollections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        CourseApprovalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseApprovals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(nullable: false),
                        NameEn = c.String(nullable: false),
                        DescriptionAr = c.String(),
                        DescriptionEn = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourseApprovals");
            DropTable("dbo.CourseApprovalCollections");
        }
    }
}
