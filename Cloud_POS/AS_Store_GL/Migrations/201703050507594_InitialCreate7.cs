namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ASL_CSMS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        TRANSYY = c.Long(nullable: false),
                        TRANSNO = c.Long(nullable: false),
                        TRANSDT = c.DateTime(),
                        SMSTP = c.String(),
                        MOBNO = c.String(maxLength: 13),
                        LANGUAGE = c.String(maxLength: 3),
                        MESSAGE = c.String(),
                        STATUS = c.String(maxLength: 7),
                        SENTTM = c.DateTime(),
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
                .PrimaryKey(t => new { t.ID, t.COMPID, t.TRANSYY, t.TRANSNO });
            
            AddColumn("dbo.AslCompanies", "SMSSENDERNM", c => c.String());
            AddColumn("dbo.GL_STRANS", "CHQPAYTO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GL_STRANS", "CHQPAYTO");
            DropColumn("dbo.AslCompanies", "SMSSENDERNM");
            DropTable("dbo.ASL_CSMS");
        }
    }
}
