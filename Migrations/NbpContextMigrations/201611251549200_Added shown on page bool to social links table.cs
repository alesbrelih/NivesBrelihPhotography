namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedshownonpagebooltosociallinkstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileLink", "ShownOnProfile", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileLink", "ShownOnProfile");
        }
    }
}
