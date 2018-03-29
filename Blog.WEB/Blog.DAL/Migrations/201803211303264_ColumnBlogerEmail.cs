namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnBlogerEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "BlogerEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "BlogerEmail");
        }
    }
}
