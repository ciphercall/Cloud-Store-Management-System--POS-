namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.STK_PS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        PSID = c.Long(nullable: false),
                        PSGRID = c.Long(nullable: false),
                        ADDRESS = c.String(maxLength: 100),
                        TELNO = c.String(maxLength: 20),
                        MOBNO = c.String(),
                        EMAIL = c.String(maxLength: 80),
                        WEBID = c.String(maxLength: 80),
                        CPNM = c.String(maxLength: 80),
                        CPCNO = c.String(maxLength: 11),
                        REMARKS = c.String(maxLength: 50),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.PSID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.STK_PS");
        }
    }
}
