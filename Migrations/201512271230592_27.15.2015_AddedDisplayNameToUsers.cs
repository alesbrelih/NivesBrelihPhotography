namespace NivesBrelihPhotography.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27152015_AddedDisplayNameToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DisplayName");
        }
    }
}
