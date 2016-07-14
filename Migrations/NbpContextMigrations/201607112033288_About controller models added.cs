namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aboutcontrollermodelsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Lastname = c.String(nullable: false, maxLength: 40),
                        ProfilePicture = c.String(nullable: false, maxLength: 100),
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
                        LinkDescription = c.String(nullable: false, maxLength: 100),
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
            DropIndex("dbo.ReferencePhoto", new[] { "PhotoId" });
            DropIndex("dbo.ReferencePhoto", new[] { "ReferenceId" });
            DropTable("dbo.Reference");
            DropTable("dbo.ReferencePhoto");
            DropTable("dbo.ProfileLink");
            DropTable("dbo.Profile");
        }
    }
}
