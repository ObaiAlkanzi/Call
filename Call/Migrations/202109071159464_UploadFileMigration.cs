namespace Call.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadFileMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Path");
        }
    }
}
