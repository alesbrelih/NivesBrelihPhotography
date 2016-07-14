namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfileLinkIconUrlrow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileLink", "LinkUrl", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileLink", "LinkUrl");
        }
    }
}
