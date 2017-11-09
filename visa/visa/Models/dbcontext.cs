using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace visa.Models
{
    public class dbcontext :DbContext
    {
        public dbcontext() : base("dbcontext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbcontext, visa.Migrations.Configuration>("dbcontext"));
        }

        public System.Data.Entity.DbSet<visa.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<visa.Models.College> Colleges { get; set; }

        public System.Data.Entity.DbSet<visa.Models.PreForm> PreForms { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Assigndata> Assigndatas { get; set; }

        public System.Data.Entity.DbSet<visa.Models.ProcessingForm> ProcessingForms { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Docs> Docs { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Gst> Gsts { get; set; }

        public System.Data.Entity.DbSet<visa.Models.Invoice> Invoices { get; set; }
    }
}