namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhotoAlbumnewPhotoProps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoAlbum",
                c => new
                    {
                        PhotoAlbumId = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false),
                        AlbumDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoAlbumId);
            
            AddColumn("dbo.Photo", "IsOnFrontPage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Photo", "PhotoAlbumId", c => c.Int());
            AddColumn("dbo.Photo", "IsPhotoAlbumCover", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Photo", "PhotoAlbumId");
            AddForeignKey("dbo.Photo", "PhotoAlbumId", "dbo.PhotoAlbum", "PhotoAlbumId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoAlbumId", "dbo.PhotoAlbum");
            DropIndex("dbo.Photo", new[] { "PhotoAlbumId" });
            DropColumn("dbo.Photo", "IsPhotoAlbumCover");
            DropColumn("dbo.Photo", "PhotoAlbumId");
            DropColumn("dbo.Photo", "IsOnFrontPage");
            DropTable("dbo.PhotoAlbum");
        }
    }
}
