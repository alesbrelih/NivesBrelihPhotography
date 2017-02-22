namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumCover",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoId = c.Int(),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoAlbum", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Photo", t => t.PhotoId)
                .Index(t => t.PhotoId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.PhotoAlbum",
                c => new
                    {
                        PhotoAlbumId = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false),
                        AlbumDate = c.DateTime(nullable: false),
                        AlbumDescription = c.String(),
                    })
                .PrimaryKey(t => t.PhotoAlbumId);
            
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
                        IsOnFrontPage = c.Boolean(nullable: false),
                        HomeCarousel = c.Boolean(nullable: false),
                        Orientation = c.String(maxLength: 1),
                        PhotoAlbumId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.PhotoAlbum", t => t.PhotoAlbumId)
                .Index(t => t.PhotoAlbumId);
            
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
                        Edited = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Photo", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
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
                        BlogDescription = c.String(nullable: false, maxLength: 400),
                        Content = c.String(nullable: false),
                        CoverPhotoId = c.Int(nullable: false),
                        AlbumLink = c.Boolean(nullable: false),
                        AlbumId = c.Int(),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.PhotoAlbum", t => t.AlbumId)
                .ForeignKey("dbo.Photo", t => t.CoverPhotoId, cascadeDelete: true)
                .Index(t => t.CoverPhotoId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.PhotoShootReview",
                c => new
                    {
                        PhotoShootReviewId = c.Int(nullable: false, identity: true),
                        ReviewerName = c.String(maxLength: 30),
                        Review = c.String(),
                    })
                .PrimaryKey(t => t.PhotoShootReviewId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Lastname = c.String(nullable: false, maxLength: 40),
                        ProfilePicture = c.String(nullable: false, maxLength: 100),
                        ContactEmail = c.String(nullable: false, maxLength: 100),
                        ContactPhone = c.String(),
                        About = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.ProfileLink",
                c => new
                    {
                        ProfileLinkId = c.Int(nullable: false, identity: true),
                        LinkName = c.String(nullable: false, maxLength: 20),
                        LinkImgUrl = c.String(nullable: false, maxLength: 100),
                        LinkUrl = c.String(nullable: false, maxLength: 100),
                        LinkDescription = c.String(nullable: false, maxLength: 100),
                        ShownOnProfile = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileLinkId);
            
            CreateTable(
                "dbo.ReferencePhoto",
                c => new
                    {
                        ReferencePhotoId = c.Int(nullable: false, identity: true),
                        ReferenceId = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReferencePhotoId)
                .ForeignKey("dbo.Photo", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.Reference", t => t.ReferenceId, cascadeDelete: true)
                .Index(t => t.ReferenceId)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Reference",
                c => new
                    {
                        ReferenceId = c.Int(nullable: false, identity: true),
                        ReferenceTitle = c.String(nullable: false, maxLength: 50),
                        ReferenceDescription = c.String(),
                    })
                .PrimaryKey(t => t.ReferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferencePhoto", "ReferenceId", "dbo.Reference");
            DropForeignKey("dbo.ReferencePhoto", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.BlogCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.BlogCategory", "BlogId", "dbo.Blog");
            DropForeignKey("dbo.Blog", "CoverPhotoId", "dbo.Photo");
            DropForeignKey("dbo.Blog", "AlbumId", "dbo.PhotoAlbum");
            DropForeignKey("dbo.AlbumCover", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.AlbumCover", "AlbumId", "dbo.PhotoAlbum");
            DropForeignKey("dbo.Photo", "PhotoAlbumId", "dbo.PhotoAlbum");
            DropForeignKey("dbo.Comment", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.PhotoCategory", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.PhotoCategory", "CategoryId", "dbo.Category");
            DropIndex("dbo.ReferencePhoto", new[] { "PhotoId" });
            DropIndex("dbo.ReferencePhoto", new[] { "ReferenceId" });
            DropIndex("dbo.Blog", new[] { "AlbumId" });
            DropIndex("dbo.Blog", new[] { "CoverPhotoId" });
            DropIndex("dbo.BlogCategory", new[] { "BlogId" });
            DropIndex("dbo.BlogCategory", new[] { "CategoryId" });
            DropIndex("dbo.Comment", new[] { "PhotoId" });
            DropIndex("dbo.PhotoCategory", new[] { "CategoryId" });
            DropIndex("dbo.PhotoCategory", new[] { "PhotoId" });
            DropIndex("dbo.Photo", new[] { "PhotoAlbumId" });
            DropIndex("dbo.AlbumCover", new[] { "AlbumId" });
            DropIndex("dbo.AlbumCover", new[] { "PhotoId" });
            DropTable("dbo.Reference");
            DropTable("dbo.ReferencePhoto");
            DropTable("dbo.ProfileLink");
            DropTable("dbo.Profile");
            DropTable("dbo.PhotoShootReview");
            DropTable("dbo.Blog");
            DropTable("dbo.BlogCategory");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
            DropTable("dbo.PhotoCategory");
            DropTable("dbo.Photo");
            DropTable("dbo.PhotoAlbum");
            DropTable("dbo.AlbumCover");
        }
    }
}
