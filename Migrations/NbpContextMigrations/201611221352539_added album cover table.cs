namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedalbumcovertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumCover",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoId = c.Int(),
                        AlbumId = c.Int(),
                        Album_PhotoAlbumId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoAlbum", t => t.Album_PhotoAlbumId)
                .ForeignKey("dbo.Photo", t => t.PhotoId)
                .Index(t => t.PhotoId)
                .Index(t => t.Album_PhotoAlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumCover", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.AlbumCover", "Album_PhotoAlbumId", "dbo.PhotoAlbum");
            DropIndex("dbo.AlbumCover", new[] { "Album_PhotoAlbumId" });
            DropIndex("dbo.AlbumCover", new[] { "PhotoId" });
            DropTable("dbo.AlbumCover");
        }
    }
}
