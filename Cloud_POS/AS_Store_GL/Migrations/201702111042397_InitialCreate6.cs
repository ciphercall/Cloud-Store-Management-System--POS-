namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ASL_PCalendarImage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Year = c.Long(nullable: false),
                        Month = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Year, t.Month });
            
            CreateTable(
                "dbo.ASL_SchedularCalendar",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        USERID = c.Long(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.COMPID, t.USERID });
            
            DropTable("dbo.ASL_CALENDAR");
        }
        
        public override void Down()
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
            
            DropTable("dbo.ASL_SchedularCalendar");
            DropTable("dbo.ASL_PCalendarImage");
        }
    }
}
