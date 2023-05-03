namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AslCompanies", "ADDRESS2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AslCompanies", "ADDRESS2");
        }
    }
}
