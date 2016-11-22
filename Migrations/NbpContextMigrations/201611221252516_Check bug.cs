namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkbug : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photo", "PhotoAlbumId", "dbo.PhotoAlbum");
            AddColumn("dbo.PhotoAlbum", "CoverPhoto_PhotoId", c => c.Int());
            AddColumn("dbo.Photo", "PhotoAlbum_PhotoAlbumId", c => c.Int());
            CreateIndex("dbo.PhotoAlbum", "CoverPhoto_PhotoId");
            CreateIndex("dbo.Photo", "PhotoAlbum_PhotoAlbumId");
            AddForeignKey("dbo.PhotoAlbum", "CoverPhoto_PhotoId", "dbo.Photo", "PhotoId");
            AddForeignKey("dbo.Photo", "PhotoAlbum_PhotoAlbumId", "dbo.PhotoAlbum", "PhotoAlbumId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoAlbum_PhotoAlbumId", "dbo.PhotoAlbum");
            DropForeignKey("dbo.PhotoAlbum", "CoverPhoto_PhotoId", "dbo.Photo");
            DropIndex("dbo.Photo", new[] { "PhotoAlbum_PhotoAlbumId" });
            DropIndex("dbo.PhotoAlbum", new[] { "CoverPhoto_PhotoId" });
            DropColumn("dbo.Photo", "PhotoAlbum_PhotoAlbumId");
            DropColumn("dbo.PhotoAlbum", "CoverPhoto_PhotoId");
            AddForeignKey("dbo.Photo", "PhotoAlbumId", "dbo.PhotoAlbum", "PhotoAlbumId");
        }
    }
}
