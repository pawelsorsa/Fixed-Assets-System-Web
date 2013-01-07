using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class MembershipRoleRepository : IRepository<MembershipRoleModel>
    {
        public IQueryable<MembershipRoleModel> Repository
        {
            get 
            {
                List<MembershipRoleModel> List = new List<MembershipRoleModel>();
                foreach(var item in Roles.GetAllRoles())
                {
                    MembershipRoleModel model = new MembershipRoleModel() { Name = item };
                    model.Users = Roles.GetUsersInRole(item);
                    List.Add(model);
                }

                return List.AsQueryable(); 
            } 
        }

        public void AddObject(MembershipRoleModel obj)
        {
            Roles.CreateRole(obj.Name);
        }

        public void DeleteObject(MembershipRoleModel obj)
        {
            Roles.DeleteRole(obj.Name);
        }

        public void EditObject(MembershipRoleModel obj)
        {
            if(!Roles.RoleExists(obj.Name)) return;

            if (Roles.GetUsersInRole(obj.Name).Count() > 0)
            {
                Roles.RemoveUsersFromRole(Roles.GetUsersInRole(obj.Name), obj.Name);
            }
            if (obj.SelectedSources.Count() > 0)
            {
                Roles.AddUsersToRole(obj.SelectedSources.ToArray(), obj.Name);
            }
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}