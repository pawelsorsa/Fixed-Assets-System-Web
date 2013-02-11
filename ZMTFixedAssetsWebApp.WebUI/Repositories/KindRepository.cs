using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class KindRepository : IRepository<Kind>
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Kind> Repository
        {
            get { return context.Kinds.AsQueryable(); } 
        }

        public void AddObject(Kind obj)
        {
            context.Kinds.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Kind obj)
        {
            context.Kinds.Attach(obj);
            context.Kinds.Remove(obj);
            context.SaveChanges();
        }

        public void EditObject(Kind obj)
        {
            context.SaveChanges();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}