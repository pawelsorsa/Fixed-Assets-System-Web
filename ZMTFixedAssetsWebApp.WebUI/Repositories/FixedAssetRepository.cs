using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;


namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class FixedAssetRepository : IRepository<FixedAsset>
    {
        private EFDbContext context = new EFDbContext();


        public IQueryable<FixedAsset> Repository
        {
            get { return context.FixedAssets.AsQueryable(); }
        }

        public void AddObject(FixedAsset obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(FixedAsset obj)
        {
            throw new NotImplementedException();
        }

        public void EditObject(FixedAsset obj)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}