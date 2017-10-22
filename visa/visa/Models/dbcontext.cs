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
    }
}