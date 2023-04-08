namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdlkjklfdxdfjkhhgh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Galleries", "LocationAr", c => c.String());
            AddColumn("dbo.Galleries", "LocationEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Galleries", "LocationEn");
            DropColumn("dbo.Galleries", "LocationAr");
        }
    }
}
