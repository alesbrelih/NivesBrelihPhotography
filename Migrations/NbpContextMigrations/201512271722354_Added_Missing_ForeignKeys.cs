namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Missing_ForeignKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comment", "Edited", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment", "Edited", c => c.DateTime(nullable: false));
        }
    }
}
