using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ZMTFixedAssetsWebApp.Domain.Abstract;
using System.Web.Security;
using ZMTFixedAssetsWebApp.WebUI.Models;


namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class MembershipUserRepository : IRepository<MembershipUserModel>
    {
        //Membership.GetAllUsers().Cast<MembershipUser>().AsQueryable<MembershipUser>(), page.HasValue ? page.Value : 0, 10);

        public IQueryable<MembershipUserModel> Repository
        {
            get 
            {
                List<MembershipUserModel> model = new List<MembershipUserModel>();

                foreach (var x in Membership.GetAllUsers().Cast<MembershipUser>().AsQueryable())
                {
                    MembershipUserModel temp = new MembershipUserModel(x.ProviderName,
                        x.UserName, x.ProviderUserKey, x.Email, x.PasswordQuestion, x.Comment, x.IsApproved,
                        x.IsLockedOut, x.CreationDate, x.LastLoginDate, x.LastActivityDate, x.LastPasswordChangedDate, x.LastLockoutDate, x.IsOnline);
                    model.Add(temp);
                }
                return model.AsQueryable(); 
            } 
        }

        public void AddObject(MembershipUserModel obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(MembershipUserModel obj)
        {
            Membership.DeleteUser(obj.UserName);
        }

        public void EditObject(MembershipUserModel obj)
        {
            MembershipUser user = Membership.GetUser(obj.UserName);
            user.Comment = obj.Comment;
            user.Email = obj.Email;
            user.IsApproved = obj.IsApproved;
            // user.LastActivityDate = model.LastActivityDate;
            // user.LastLoginDate = model.LastLoginDate;
            Membership.UpdateUser(user);

            if (Roles.GetRolesForUser(obj.UserName).Count() > 0)
            {
                Roles.RemoveUserFromRoles(obj.UserName, Roles.GetRolesForUser(obj.UserName));
            }
            if (obj.SelectedSources.Count() > 0)
            {
                Roles.AddUserToRoles(obj.UserName, obj.SelectedSources.ToArray());
            }
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}