﻿using System;
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
           //Database.SetInitializer<DbContext>(new CreateDatabaseIfNotExists<DbContext>());
 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Section>().HasKey(x => x.id);
            modelBuilder.Entity<Licence>().HasKey(x => x.id_number);
            modelBuilder.Entity<MembershipUser>().HasKey(x => x.login);
            modelBuilder.Entity<Person>().HasKey(x => x.id);


            // Identity: None
            modelBuilder.Entity<Person>().Property(e => e.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None);


            // one to many  One Section Many Persons
            modelBuilder.Entity<Person>()
            .HasRequired(p => p.Section)
            .WithMany(u => u.People)
            .HasForeignKey(x => x.id_section).WillCascadeOnDelete();

        }
    }
}