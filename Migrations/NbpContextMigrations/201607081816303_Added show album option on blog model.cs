namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedshowalbumoptiononblogmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "AlbumLink", c => c.Boolean(nullable: false));
            AddColumn("dbo.Blog", "AlbumId", c => c.Int());
            CreateIndex("dbo.Blog", "AlbumId");
            AddForeignKey("dbo.Blog", "AlbumId", "dbo.PhotoAlbum", "PhotoAlbumId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog", "AlbumId", "dbo.PhotoAlbum");
            DropIndex("dbo.Blog", new[] { "AlbumId" });
            DropColumn("dbo.Blog", "AlbumId");
            DropColumn("dbo.Blog", "AlbumLink");
        }
    }
}
