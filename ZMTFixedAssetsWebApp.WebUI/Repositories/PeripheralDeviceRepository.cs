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
            context.PeripheralDevices.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(PeripheralDevice obj)
        {
            context.PeripheralDevices.Attach(obj);
            context.PeripheralDevices.Remove(obj);
            context.SaveChanges();
        }

        public void EditObject(PeripheralDevice obj)
        {
            context.SaveChanges();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}