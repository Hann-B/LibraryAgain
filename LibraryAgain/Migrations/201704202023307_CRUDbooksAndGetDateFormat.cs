namespace LibraryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CRUDbooksAndGetDateFormat : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "DueBackDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "DueBackDate", c => c.DateTime());
        }
    }
}
