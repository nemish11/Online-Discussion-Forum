namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "like", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "dislike", c => c.Int(nullable: false));
            AddColumn("dbo.Fourms", "datetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fourms", "datetime");
            DropColumn("dbo.Comments", "dislike");
            DropColumn("dbo.Comments", "like");
            DropColumn("dbo.Comments", "datetime");
        }
    }
}
