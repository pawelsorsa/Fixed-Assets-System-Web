using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class EFDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Section> Sections { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Section>().HasKey(x => x.id);
            modelBuilder.Entity<Licence>().HasKey(x => x.id_number);
            modelBuilder.Entity<MembershipUser>().HasKey(x => x.login);
            modelBuilder.Entity<Person>().HasKey(x => x.id);




        }
    }
}