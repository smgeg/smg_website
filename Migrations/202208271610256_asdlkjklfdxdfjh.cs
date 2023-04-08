namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdlkjklfdxdfjh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Galleries", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Galleries", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Galleries", "CreatedBy", c => c.String());
            AddColumn("dbo.Galleries", "UpdatedBy", c => c.String());
            DropColumn("dbo.Galleries", "_date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Galleries", "_date", c => c.String());
            DropColumn("dbo.Galleries", "UpdatedBy");
            DropColumn("dbo.Galleries", "CreatedBy");
            DropColumn("dbo.Galleries", "UpdatedOn");
            DropColumn("dbo.Galleries", "CreatedOn");
        }
    }
}
