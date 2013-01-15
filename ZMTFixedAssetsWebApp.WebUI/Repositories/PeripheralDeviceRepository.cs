using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class PeripheralDeviceRepository : IRepository<PeripheralDevice>
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<PeripheralDevice> Repository
        {
            get { return context.PeripheralDevices.AsQueryable(); } 
        }

        public void AddObject(PeripheralDevice obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(PeripheralDevice obj)
        {
            throw new NotImplementedException();
        }

        public void EditObject(PeripheralDevice obj)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}