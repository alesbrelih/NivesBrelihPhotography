namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedalbumcovertable2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AlbumCover", "AlbumId");
            RenameColumn(table: "dbo.AlbumCover", name: "Album_PhotoAlbumId", newName: "AlbumId");
            RenameIndex(table: "dbo.AlbumCover", name: "IX_Album_PhotoAlbumId", newName: "IX_AlbumId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AlbumCover", name: "IX_AlbumId", newName: "IX_Album_PhotoAlbumId");
            RenameColumn(table: "dbo.AlbumCover", name: "AlbumId", newName: "Album_PhotoAlbumId");
            AddColumn("dbo.AlbumCover", "AlbumId", c => c.Int());
        }
    }
}
