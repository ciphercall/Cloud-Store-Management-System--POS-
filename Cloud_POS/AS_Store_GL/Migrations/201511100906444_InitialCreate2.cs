namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ASL_CALENDAR",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        USERID = c.Long(),
                        text = c.String(),
                        start_date = c.DateTime(),
                        end_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ASL_CALENDAR");
        }
    }
}