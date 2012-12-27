using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using System.Web.Security;
using ZMTFixedAssetsWebApp.WebUI.ListViews;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class MembershipUserController : Controller
    {
        private IRepository<MembershipUser> membershipUserRepository;
        private MembershipUserListView model;
        public MembershipUserController(IRepository<MembershipUser> membershipUserRepository)
        {
            this.membershipUserRepository = membershipUserRepository;
          //  model = new MembershipUserListView(membershipUserRepository);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}
