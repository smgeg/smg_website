namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgricultureNetworkUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(),
                        NameEn = c.String(),
                        MobileNo = c.String(),
                        Image = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AgricultureNetworkUsers");
        }
    }
}
