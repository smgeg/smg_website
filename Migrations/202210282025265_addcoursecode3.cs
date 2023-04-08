namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoursecode3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Code", c => c.String());
            AddColumn("dbo.Trainings", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "Code");
            DropColumn("dbo.Courses", "Code");
        }
    }
}
