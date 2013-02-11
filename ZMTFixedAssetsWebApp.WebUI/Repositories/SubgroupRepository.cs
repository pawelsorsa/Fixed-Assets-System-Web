using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class SubgroupRepository : IRepository<Subgroup>
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Subgroup> Repository
        {
            get { return context.Subgroups.AsQueryable(); }
        }

        public void AddObject(Subgroup obj)
        {
            context.Subgroups.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Subgroup obj)
        {
            context.Subgroups.Attach(obj);
            context.Subgroups.Remove(obj);
            context.SaveChanges();
        }

        public void EditObject(Subgroup obj)
        {
            context.SaveChanges();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}