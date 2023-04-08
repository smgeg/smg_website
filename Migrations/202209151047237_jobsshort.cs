namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobsshort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ShortDescriptionAr", c => c.String());
            AddColumn("dbo.Jobs", "ShortDescriptionEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "ShortDescriptionEn");
            DropColumn("dbo.Jobs", "ShortDescriptionAr");
        }
    }
}
