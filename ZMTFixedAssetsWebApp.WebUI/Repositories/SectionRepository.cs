using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;


namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class SectionRepository : IRepository<Section>
    {
        private EFDbContext context = new EFDbContext();


        public IQueryable<Section> Repository
        {
            get { return context.Sections.AsQueryable(); }
        }

        public void AddObject(Section obj)
        {
            context.Sections.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Section obj)
        {
            Section temp = context.Sections.FirstOrDefault(x => x.short_name == obj.short_name);
            ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(temp, System.Data.EntityState.Deleted);
            context.Sections.Remove(temp);
            context.SaveChanges();
        }

        public void EditObject(Section obj)
        {
           // ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(obj, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public int SaveChanges()
        {
           return context.SaveChanges();
        }
    }
}