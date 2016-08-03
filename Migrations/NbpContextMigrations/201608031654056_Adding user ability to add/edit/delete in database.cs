namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addinguserabilitytoaddeditdeleteindatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminPhotoIndexVm",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Album = c.String(),
                        OnPortfolio = c.Boolean(nullable: false),
                        Uploaded = c.DateTime(nullable: false),
                        PhotoUrl = c.String(),
                        PhotoTitle = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminPhotoIndexVm");
        }
    }
}
