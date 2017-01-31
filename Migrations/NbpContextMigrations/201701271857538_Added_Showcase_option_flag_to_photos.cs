namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Showcase_option_flag_to_photos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "OnShowCase", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "OnShowCase");
        }
    }
}
