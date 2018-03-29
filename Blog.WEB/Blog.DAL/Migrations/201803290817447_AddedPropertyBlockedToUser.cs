namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertyBlockedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Blocked");
        }
    }
}
