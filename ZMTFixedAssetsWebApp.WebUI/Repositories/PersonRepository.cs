using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Collections;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Person> People
        {
            get { return context.Persons.AsQueryable(); }
        }


        public void AddPerson(Person person)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }


        public void EditPerson(Person person)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.SaveChanges();
            }
        }


        public void DeletePerson(int id)
        {

            using (EFDbContext context = new EFDbContext())
            {
                Person ppp = new Person() { id = id };
                context.Persons.Attach(ppp);
                context.Persons.Remove(ppp);
                context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.SaveChanges();
            }
        }


    }
}