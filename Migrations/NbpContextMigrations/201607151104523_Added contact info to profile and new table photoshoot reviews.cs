namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcontactinfotoprofileandnewtablephotoshootreviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoShootReview",
                c => new
                    {
                        PhotoShootReviewId = c.Int(nullable: false, identity: true),
                        ReviewerName = c.String(maxLength: 30),
                        Review = c.String(),
                    })
                .PrimaryKey(t => t.PhotoShootReviewId);
            
            AddColumn("dbo.Profile", "ContactEmail", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Profile", "ContactPhone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "ContactPhone");
            DropColumn("dbo.Profile", "ContactEmail");
            DropTable("dbo.PhotoShootReview");
        }
    }
}
