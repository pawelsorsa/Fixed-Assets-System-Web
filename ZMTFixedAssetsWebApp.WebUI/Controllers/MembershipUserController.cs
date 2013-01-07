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
    [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles = "Admins")]
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
            if (Request.IsAjaxRequest())
            {
                return PartialView("MembershipUser/_MembershipUserIndex", membershipUserListView.CreateListModel(1, false, "UserName", 10, false, false, ""));
            }
            return View(membershipUserListView.CreateListModel(1, false, "UserName", 10, false, false, ""));
        }


        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "UserName", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("MembershipUser/_MembershipUserList", membershipUserListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(membershipUserListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<MembershipUserModel> model, FormCollection collection)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        [HttpGet]
        public ActionResult Edit(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            if (user != null)
            {
                MembershipUserModel model = new MembershipUserModel(user.ProviderName,
                            user.UserName, user.ProviderUserKey, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved,
                            user.IsLockedOut, user.CreationDate, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate, user.LastLockoutDate, user.IsOnline);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipUser/_MembershipUserEdit", model);
                }

                return View(model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podany użytkownik nie istnieje",
                    Action = "Index",
                    Controller = "MembershipUser"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles = "Admins")]
        [HttpPost]
        public ActionResult Edit(MembershipUserModel model)
        {
            membershipUserRepository.EditObject(model);
            return RedirectToAction("Index");
        }

        [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles = "Admins")]
        [HttpGet]
        public ActionResult Delete(string username)
        {
            MembershipUserModel user = membershipUserRepository.Repository.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                DeleteObjectByName model = new DeleteObjectByName();
                model.Description = "Czy napewno chcesz usunąć użytkownika: " + user.UserName + "?";
                model.Name = user.UserName;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipUser/_MembershipUserDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podany użytkownik nie istnieje",
                    Action = "Index",
                    Controller = "MembershipUser"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }


        [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles = "Admins")]
        [HttpPost]
        public ActionResult Delete(DeleteObjectByName model)
        {
            if (ModelState.IsValid)
            {
                MembershipUserModel m = membershipUserRepository.Repository.FirstOrDefault(x => x.UserName == model.Name);
                membershipUserRepository.DeleteObject(m);
                return RedirectToAction("Index");
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipUser/_MembershipUserDelete", model);
                }
                return View("Delete", model);
            }
        }

        public JsonResult SortByList()
        {
            IQueryable list = membershipUserListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }



    }
}
