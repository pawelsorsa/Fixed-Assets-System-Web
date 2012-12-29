using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class MembershipUserController : Controller
    {

        private IRepository<MembershipUserModel> membershipUserRepository;
        private MembershipUserListView membershipUserListView;
        public MembershipUserController(IRepository<MembershipUserModel> membershipUserRepository)
        {
            this.membershipUserRepository = membershipUserRepository;
            membershipUserListView = new MembershipUserListView(membershipUserRepository);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(membershipUserListView.CreateListModel(1, false, "UserName", 10, false, false, ""));
        }


        [HttpGet]
        public ActionResult Edit(string username)
        {
            MembershipUser x = Membership.GetUser(username);
            MembershipUserModel model = new MembershipUserModel(x.ProviderName,
                        x.UserName, x.ProviderUserKey, x.Email, x.PasswordQuestion, x.Comment, x.IsApproved,
                        x.IsLockedOut, x.CreationDate, x.LastLoginDate, x.LastActivityDate, x.LastPasswordChangedDate, x.LastLockoutDate, x.IsOnline);
                    
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MembershipUserModel model)
        {
            MembershipUser user = Membership.GetUser(model.UserName);
            user.Comment = model.Comment;
            user.Email = model.Email;
            user.IsApproved = model.IsApproved;
           // user.LastActivityDate = model.LastActivityDate;
           // user.LastLoginDate = model.LastLoginDate;
            Membership.UpdateUser(user);

            Roles.RemoveUserFromRoles(model.UserName, Roles.GetRolesForUser(model.UserName));
            Roles.AddUserToRoles(model.UserName, model.SelectedSources.ToArray());

            return RedirectToAction("List");
        }
       

    }
}
