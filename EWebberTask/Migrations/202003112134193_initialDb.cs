namespace EWebberTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ISBN = c.String(),
                        Count = c.Int(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.StudentBook",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Retrived = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentBook", "StudentId", "dbo.Student");
            DropForeignKey("dbo.StudentBook", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "Author_Id", "dbo.Author");
            DropIndex("dbo.StudentBook", new[] { "BookId" });
            DropIndex("dbo.StudentBook", new[] { "StudentId" });
            DropIndex("dbo.Book", new[] { "Author_Id" });
            DropTable("dbo.Student");
            DropTable("dbo.StudentBook");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
