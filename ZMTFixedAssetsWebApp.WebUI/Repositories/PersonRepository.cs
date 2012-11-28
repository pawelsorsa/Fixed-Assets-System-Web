using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Person> People
        {
            get { return context.Persons; }
        }


        public void AddPerson(Person person)
        {
            context.Persons.Add(person);
            context.SaveChanges();
        }


        public void EditPerson(Person person)
        {
           // context.Entry(person).State = System.Data.EntityState.Detached;
           // context.Persons.Attach(person);
           // context.Entry(person).State = System.Data.EntityState.Modified;
            context.SaveChanges();
           
        }


        public void DeletePerson(Person person)
        {
         //   context.Persons.(person);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


    }
}