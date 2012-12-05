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
                context.SaveChanges();
            }
            catch (UpdateException ex)
            {
                
                throw new UpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public void EditPerson(Person person)
        {
           // context.Entry(person).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }


        public void DeletePerson(Person person)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    Person ppp = new Person() { id = person.id }; 
                   // context.Entry(person).State = System.Data.EntityState.Deleted;
                    ((IObjectContextAdapter)context).ObjectContext.AttachTo("Persons", ppp);
                    ((IObjectContextAdapter)context).ObjectContext.DeleteObject(ppp);
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