namespace AS_Store_GL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AslCompanies",
                c => new
                    {
                        AslCompanyId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COMPNM = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        CONTACTNO = c.String(nullable: false),
                        EMAILID = c.String(nullable: false),
                        WEBID = c.String(),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.AslCompanyId);
            
            CreateTable(
                "dbo.ASL_DELETE",
                c => new
                    {
                        Asl_DeleteID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        DELSLNO = c.Long(),
                        DELDATE = c.String(),
                        DELTIME = c.String(),
                        DELIPNO = c.String(),
                        DELLTUDE = c.String(),
                        TABLEID = c.String(),
                        DELDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_DeleteID);
            
            CreateTable(
                "dbo.ASL_LOG",
                c => new
                    {
                        Asl_LOGid = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        LOGTYPE = c.String(),
                        LOGSLNO = c.Long(),
                        LOGDATE = c.DateTime(),
                        LOGTIME = c.String(),
                        LOGIPNO = c.String(),
                        LOGLTUDE = c.String(),
                        TABLEID = c.String(),
                        LOGDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_LOGid);
            
            CreateTable(
                "dbo.ASL_MENU",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MODULEID = c.String(),
                        MENUTP = c.String(),
                        MENUID = c.String(),
                        MENUNM = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        SERIAL = c.Long(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ASL_MENUMST",
                c => new
                    {
                        MODULEID = c.String(nullable: false, maxLength: 128),
                        MODULENM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.MODULEID);
            
            CreateTable(
                "dbo.ASL_ROLE",
                c => new
                    {
                        ASL_ROLEId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        USERID = c.Long(nullable: false),
                        MODULEID = c.String(nullable: false),
                        MENUTP = c.String(nullable: false),
                        MENUID = c.String(),
                        SERIAL = c.Long(nullable: false),
                        STATUS = c.String(),
                        INSERTR = c.String(),
                        UPDATER = c.String(),
                        DELETER = c.String(),
                        MENUNAME = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.ASL_ROLEId);
            
            CreateTable(
                "dbo.AslUsercoes",
                c => new
                    {
                        AslUsercoId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        USERNM = c.String(nullable: false),
                        DEPTNM = c.String(),
                        OPTP = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        MOBNO = c.String(nullable: false),
                        EMAILID = c.String(),
                        LOGINBY = c.String(nullable: false),
                        LOGINID = c.String(nullable: false),
                        LOGINPW = c.String(nullable: false),
                        TIMEFR = c.String(nullable: false),
                        TIMETO = c.String(nullable: false),
                        STATUS = c.String(nullable: false),
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
                .PrimaryKey(t => t.AslUsercoId);
            
            CreateTable(
                "dbo.GL_ACCHARMST",
                c => new
                    {
                        ACCHARMSTId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        HEADTP = c.Int(nullable: false),
                        HEADCD = c.Long(),
                        HEADNM = c.String(nullable: false),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ACCHARMSTId);
            
            CreateTable(
                "dbo.GL_ACCHART",
                c => new
                    {
                        ACCHARTId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        HEADTP = c.Int(nullable: false),
                        HEADCD = c.Long(),
                        ACCOUNTCD = c.Long(),
                        ACCOUNTNM = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ACCHARTId);
            
            CreateTable(
                "dbo.GL_COSTPMST",
                c => new
                    {
                        CostMstID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTCNM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CostMstID);
            
            CreateTable(
                "dbo.GL_COSTPOOL",
                c => new
                    {
                        CostPLID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTPID = c.Long(),
                        COSTPNM = c.String(nullable: false),
                        COSTPSID = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CostPLID);
            
            CreateTable(
                "dbo.GL_MASTER",
                c => new
                    {
                        GL_MasterID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSSL = c.Long(),
                        TRANSDRCR = c.String(),
                        TRANSFOR = c.String(),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        DEBITAMT = c.Decimal(precision: 18, scale: 2),
                        CREDITAMT = c.Decimal(precision: 18, scale: 2),
                        TABLEID = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.GL_MasterID);
            
            CreateTable(
                "dbo.GL_STRANS",
                c => new
                    {
                        Gl_StransID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSFOR = c.String(nullable: false),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Gl_StransID);
            
            CreateTable(
                "dbo.STK_ITEM",
                c => new
                    {
                        STK_ITEM_ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        CATID = c.Long(),
                        ITEMID = c.Long(),
                        ITEMNM = c.String(),
                        BRAND = c.String(),
                        UNIT = c.String(),
                        BUYRT = c.Decimal(precision: 18, scale: 2),
                        SALRT = c.Decimal(precision: 18, scale: 2),
                        STKMIN = c.Decimal(precision: 18, scale: 2),
                        DISCRT = c.Decimal(precision: 18, scale: 2),
                        CPQTY = c.Decimal(precision: 18, scale: 2),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.STK_ITEM_ID);
            
            CreateTable(
                "dbo.STK_ITEMMST",
                c => new
                    {
                        STK_ITEMMST_ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        CATID = c.Long(),
                        CATNM = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.STK_ITEMMST_ID);
            
            CreateTable(
                "dbo.STK_STORE",
                c => new
                    {
                        STK_STORE_ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        STOREID = c.Long(),
                        STORENM = c.String(nullable: false),
                        STORESID = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.STK_STORE_ID);
            
            CreateTable(
                "dbo.STK_TRANS",
                c => new
                    {
                        STK_TRANS_ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(),
                        TRANSYY = c.Long(),
                        TRANSNO = c.Long(),
                        STOREFR = c.Long(),
                        STORETO = c.Long(),
                        PSID = c.Long(),
                        ITEMSL = c.Long(),
                        ITEMID = c.Long(),
                        QTY = c.Decimal(precision: 18, scale: 2),
                        RATE = c.Decimal(precision: 18, scale: 2),
                        AMOUNT = c.Decimal(precision: 18, scale: 2),
                        DISCRT = c.Decimal(precision: 18, scale: 2),
                        DISCAMT = c.Decimal(precision: 18, scale: 2),
                        GROSSAMT = c.Decimal(precision: 18, scale: 2),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.STK_TRANS_ID);
            
            CreateTable(
                "dbo.STK_TRANSMST",
                c => new
                    {
                        STK_TRANSMST_ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(),
                        TRANSYY = c.Long(),
                        TRANSNO = c.Long(),
                        STOREFR = c.Long(),
                        STORETO = c.Long(),
                        PSID = c.Long(),
                        TOTAMT = c.Decimal(precision: 18, scale: 2),
                        DISCRTG = c.Decimal(precision: 18, scale: 2),
                        DISCAMTG = c.Decimal(precision: 18, scale: 2),
                        TOTGROSS = c.Decimal(precision: 18, scale: 2),
                        VATRT = c.Decimal(precision: 18, scale: 2),
                        VATAMT = c.Decimal(precision: 18, scale: 2),
                        OTCAMT = c.Decimal(precision: 18, scale: 2),
                        TOTNET = c.Decimal(precision: 18, scale: 2),
                        CASHAMT = c.Decimal(precision: 18, scale: 2),
                        CREDITAMT = c.Decimal(precision: 18, scale: 2),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.STK_TRANSMST_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.STK_TRANSMST");
            DropTable("dbo.STK_TRANS");
            DropTable("dbo.STK_STORE");
            DropTable("dbo.STK_ITEMMST");
            DropTable("dbo.STK_ITEM");
            DropTable("dbo.GL_STRANS");
            DropTable("dbo.GL_MASTER");
            DropTable("dbo.GL_COSTPOOL");
            DropTable("dbo.GL_COSTPMST");
            DropTable("dbo.GL_ACCHART");
            DropTable("dbo.GL_ACCHARMST");
            DropTable("dbo.AslUsercoes");
            DropTable("dbo.ASL_ROLE");
            DropTable("dbo.ASL_MENUMST");
            DropTable("dbo.ASL_MENU");
            DropTable("dbo.ASL_LOG");
            DropTable("dbo.ASL_DELETE");
            DropTable("dbo.AslCompanies");
        }
    }
}
