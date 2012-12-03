using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;
using System.Data;

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
            try
            {
                context.Persons.Add(person);     
                //context.Entry(person).State = System.Data.EntityState.Added;
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
        }


        public void EditPerson(Person person)
        {
           // context.Entry(person).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }


        public void DeletePerson(Person person)
        {
           // context.Entry(person).State = System.Data.EntityState.Deleted;
           // ((IObjectContextAdapter)context).ObjectContext.Detach(person);
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    //context.Persons.Attach(person);
                   // context.Entry(person).State = System.Data.EntityState.Deleted;
                    //((IObjectContextAdapter)context).ObjectContext.DeleteObject(person);
                    context.Persons.Attach(person);
                    context.Persons.Remove(person);
                    context.SaveChanges();

                }
                catch (UpdateException)
                {
                    throw new UpdateException(string.Format(
                    "Nie udało się edytować pracownika. Popraw dane i spróbuj ponownie"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
               
            }
            
            
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


    }
}