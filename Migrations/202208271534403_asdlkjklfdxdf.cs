namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdlkjklfdxdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Date = c.DateTime(nullable: false),
                        _date = c.String(),
                        NameAr = c.String(),
                        NameEn = c.String(),
                        DescriptionAr = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GalleryId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Galleries");
        }
    }
}
