namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobsusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CV = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        JobId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobUsers");
        }
    }
}
