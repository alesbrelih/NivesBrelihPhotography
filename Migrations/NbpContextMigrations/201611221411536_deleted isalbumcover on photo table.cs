namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedisalbumcoveronphototable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photo", "IsPhotoAlbumCover");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photo", "IsPhotoAlbumCover", c => c.Boolean(nullable: false));
        }
    }
}
