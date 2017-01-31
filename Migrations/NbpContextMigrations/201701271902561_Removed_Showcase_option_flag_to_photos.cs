namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Showcase_option_flag_to_photos : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photo", "OnShowCase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photo", "OnShowCase", c => c.Boolean(nullable: false));
        }
    }
}
