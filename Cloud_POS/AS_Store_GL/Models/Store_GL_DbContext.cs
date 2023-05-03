using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using AS_Store_GL.Models;
using AS_Store_GL.Models.ASL;
using AS_Store_GL.Models.Store;

namespace AS_Store_GL.Models
{
    public class Store_GL_DbContext : DbContext
    {
        public DbSet<AslCompany> AslCompanyDbSet { get; set; }
        public DbSet<AslUserco> AslUsercoDbSet { get; set; }
        public DbSet<ASL_LOG> AslLogDbSet { get; set; }
        public DbSet<ASL_DELETE> AslDeleteDbSet { get; set; }
        public DbSet<ASL_MENUMST> AslMenumstDbSet { get; set; }
        public DbSet<ASL_MENU> AslMenuDbSet { get; set; }
        public DbSet<ASL_ROLE> AslRoleDbSet { get; set; }
        public DbSet<GL_ACCHARMST> GlAccharmstDbSet { get; set; }
        public DbSet<GL_ACCHART> GlAcchartDbSet { get; set; }
        public DbSet<GL_COSTPMST> GLCostPMSTDbSet { get; set; }
        public DbSet<GL_COSTPOOL> GlCostPoolDbSet { get; set; }
        public DbSet<GL_STRANS> GlStransDbSet { get; set; }
        public DbSet<GL_MASTER> GlMasterDbSet { get; set; }
        public DbSet<STK_ITEMMST> StkItemmstDbSet { get; set; }
        public DbSet<STK_ITEM> StkItemDbSet { get; set; }
        public DbSet<STK_STORE> StkStoreDbSet { get; set; }
        public DbSet<STK_TRANSMST> StkTransmstDbSet { get; set; }
        public DbSet<STK_TRANS> StkTransDbSet { get; set; }
        public DbSet<STK_PS> PsDbSet { get; set; }

        //Upload Excel File Data module
        public DbSet<ASL_PCONTACTS> UploadContactDbSet { get; set; }
        public DbSet<ASL_PGROUPS> UploadGroupDbSet { get; set; }
        public DbSet<ASL_PEMAIL>SendLogEmailDbSet { get; set; }
        public DbSet<ASL_PSMS> SendLogSMSDbSet { get; set; }

        

        //Promotional
        public DbSet<ASL_PCalendarImage> CalendarImageDbSet { get; set; }
        public DbSet<ASL_SchedularCalendar> SchedularCalendarDbSet { get; set; }


        //Custom SMS 
        public DbSet<ASL_CSMS> CustomSmsDbSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}