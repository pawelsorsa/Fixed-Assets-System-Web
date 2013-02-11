using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Data.Entity.Infrastructure;

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
            context.Devices.Add(obj);
            context.SaveChanges(); 
        }

        public void DeleteObject(Device obj)
        {
            Device temp = context.Devices.FirstOrDefault(x => x.id == obj.id);
            ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(temp, System.Data.EntityState.Deleted);
            context.Devices.Remove(temp);
            context.SaveChanges();
        }

        public void EditObject(Device obj)
        {
            context.SaveChanges();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}