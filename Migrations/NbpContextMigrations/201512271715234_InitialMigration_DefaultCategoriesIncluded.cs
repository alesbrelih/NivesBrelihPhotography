namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration_DefaultCategoriesIncluded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        PhotoId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Edited = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Photo", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        PhotoTitle = c.String(nullable: false),
                        PhotoText = c.String(),
                        PhotoUrl = c.String(nullable: false),
                        Uploaded = c.DateTime(nullable: false),
                        CommentsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.PhotoCategory",
                c => new
                    {
                        PhotoCategoryId = c.Int(nullable: false, identity: true),
                        PhotoId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoCategoryId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Photo", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoCategory", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.PhotoCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Comment", "PhotoId", "dbo.Photo");
            DropIndex("dbo.PhotoCategory", new[] { "CategoryId" });
            DropIndex("dbo.PhotoCategory", new[] { "PhotoId" });
            DropIndex("dbo.Comment", new[] { "PhotoId" });
            DropTable("dbo.PhotoCategory");
            DropTable("dbo.Photo");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
        }
    }
}
