namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentsID = c.Int(nullable: false, identity: true),
                        commenttext = c.String(nullable: false),
                        uname = c.String(),
                        FourmsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentsID)
                .ForeignKey("dbo.Fourms", t => t.FourmsID, cascadeDelete: true)
                .Index(t => t.FourmsID);
            
            CreateTable(
                "dbo.Fourms",
                c => new
                    {
                        FourmsID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        text = c.String(nullable: false),
                        UsersID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FourmsID)
                .ForeignKey("dbo.Users", t => t.UsersID, cascadeDelete: true)
                .Index(t => t.UsersID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsersID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        uname = c.String(nullable: false),
                        password = c.String(nullable: false),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.UsersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "FourmsID", "dbo.Fourms");
            DropForeignKey("dbo.Fourms", "UsersID", "dbo.Users");
            DropIndex("dbo.Fourms", new[] { "UsersID" });
            DropIndex("dbo.Comments", new[] { "FourmsID" });
            DropTable("dbo.Users");
            DropTable("dbo.Fourms");
            DropTable("dbo.Comments");
        }
    }
}
