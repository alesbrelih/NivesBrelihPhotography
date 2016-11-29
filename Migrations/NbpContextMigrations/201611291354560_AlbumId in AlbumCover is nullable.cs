namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumIdinAlbumCoverisnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlbumCover", "AlbumId", "dbo.PhotoAlbum");
            DropIndex("dbo.AlbumCover", new[] { "AlbumId" });
            AlterColumn("dbo.AlbumCover", "AlbumId", c => c.Int(nullable: false));
            CreateIndex("dbo.AlbumCover", "AlbumId");
            AddForeignKey("dbo.AlbumCover", "AlbumId", "dbo.PhotoAlbum", "PhotoAlbumId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumCover", "AlbumId", "dbo.PhotoAlbum");
            DropIndex("dbo.AlbumCover", new[] { "AlbumId" });
            AlterColumn("dbo.AlbumCover", "AlbumId", c => c.Int());
            CreateIndex("dbo.AlbumCover", "AlbumId");
            AddForeignKey("dbo.AlbumCover", "AlbumId", "dbo.PhotoAlbum", "PhotoAlbumId");
        }
    }
}
