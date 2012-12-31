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
    public class PersonRepository : IRepository<Person>
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Person> Repository
        {
            get { return context.Persons.AsQueryable(); } 
        }

        public void AddObject(Person obj)
        {
            context.Persons.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Person obj)
        {
            Person person = new Person() { id = obj.id };
            context.Persons.Attach(person);
            context.Persons.Remove(person);
            context.SaveChanges();
        }

        public void EditObject(Person obj)
        {
            context.SaveChanges();
        }

        int IRepository<Person>.SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}