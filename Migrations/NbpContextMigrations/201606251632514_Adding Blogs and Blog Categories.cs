namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBlogsandBlogCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategory",
                c => new
                    {
                        BlogCategoryId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogCategoryId)
                .ForeignKey("dbo.Blog", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        BlogTitle = c.String(nullable: false, maxLength: 50),
                        BlogDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        CoverPhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Photo", t => t.CoverPhotoId, cascadeDelete: true)
                .Index(t => t.CoverPhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.BlogCategory", "BlogId", "dbo.Blog");
            DropForeignKey("dbo.Blog", "CoverPhotoId", "dbo.Photo");
            DropIndex("dbo.Blog", new[] { "CoverPhotoId" });
            DropIndex("dbo.BlogCategory", new[] { "BlogId" });
            DropIndex("dbo.BlogCategory", new[] { "CategoryId" });
            DropTable("dbo.Blog");
            DropTable("dbo.BlogCategory");
        }
    }
}
