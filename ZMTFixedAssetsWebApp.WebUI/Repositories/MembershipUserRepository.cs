using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;


namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class MembershipUserRepository : IRepository<MembershipUser>
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<MembershipUser> Repository
        {
            get { return context.MembershipUsers.AsQueryable(); } 
        }

        public void AddObject(MembershipUser obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(int id)
        {
            throw new NotImplementedException();
        }

        public void EditObject(MembershipUser obj)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}