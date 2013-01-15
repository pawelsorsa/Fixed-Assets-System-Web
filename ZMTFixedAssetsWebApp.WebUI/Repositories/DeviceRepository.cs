using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class DeviceRepository : IRepository<Device>
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Device> Repository
        {
            get { return context.Devices.AsQueryable(); }
        }

        public void AddObject(Device obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(Device obj)
        {
            throw new NotImplementedException();
        }

        public void EditObject(Device obj)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}