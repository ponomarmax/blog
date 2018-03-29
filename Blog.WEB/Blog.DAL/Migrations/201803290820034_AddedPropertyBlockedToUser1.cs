namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertyBlockedToUser1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Blocked");
            AddColumn("dbo.AspNetUsers", "Blocked", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Blocked");
        }
    }
}
