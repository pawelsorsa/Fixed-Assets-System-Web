using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

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
            throw new NotImplementedException();
        }

        public void DeleteObject(Section obj)
        {
            throw new NotImplementedException();
        }

        public void EditObject(Section obj)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}