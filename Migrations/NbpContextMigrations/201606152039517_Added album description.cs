namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedalbumdescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoAlbum", "AlbumDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoAlbum", "AlbumDescription");
        }
    }
}
