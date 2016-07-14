namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdditionalblogdescriptiontoblog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blog", "BlogDescription", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blog", "BlogDescription", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
