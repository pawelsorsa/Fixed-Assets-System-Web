using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Section> Sections
        {
            get { return context.Sections.AsQueryable(); }
        }
    }
}