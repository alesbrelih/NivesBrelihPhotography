namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedhomecarouselflagonphotomodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "HomeCarousel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "HomeCarousel");
        }
    }
}
