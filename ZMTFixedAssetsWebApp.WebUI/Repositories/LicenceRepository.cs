using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class LicenceRepository : IRepository<Licence>
    {
        private EFDbContext context = new EFDbContext();


        public IQueryable<Licence> Repository
        {
            get { return context.Licences.AsQueryable(); } 
        }

        public void AddObject(Licence obj)
        {
            context.Licences.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Licence obj)
        {
            context.Licences.Attach(obj);
            context.Licences.Remove(obj);
            context.SaveChanges();
        }

        public void EditObject(Licence obj)
        {
            context.SaveChanges();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}