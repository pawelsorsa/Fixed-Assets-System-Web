using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles = "Admins")]
    public class MembershipRoleController : Controller
    {
        //
        // GET: /MembershipRole/
        private IRepository<MembershipRoleModel> membershipRoleRepository;
        private MembershipRoleListView membershipRoleListView;

        public MembershipRoleController(IRepository<MembershipRoleModel> membershipRoleRepository)
        {
            this.membershipRoleRepository = membershipRoleRepository;
            this.membershipRoleListView = new MembershipRoleListView(membershipRoleRepository);
        }


        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("MembershipRole/_MembershipRoleIndex", membershipRoleListView.CreateListModel(1, false, "Name", 10, false, false, ""));
            }
            return View(membershipRoleListView.CreateListModel(1, false, "Name", 10, false, false, ""));
        }



        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "Name", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("MembershipRole/_MembershipRoleList", membershipRoleListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(membershipRoleListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<MembershipRoleModel> model, FormCollection collection)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }

        [HttpGet]
        public ActionResult Edit(string RoleName)
        {
            MembershipRoleModel model = membershipRoleRepository.Repository.FirstOrDefault(x => x.Name == RoleName);
            model.LoadCheckBoxList();
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipRole/_MembershipRoleEdit", model);
                }
                return View(model);
            }
            else
            {
                InfoModel info_model = new InfoModel()
                {
                    Description = "Rola nie istnieje",
                    Action = "Index",
                    Controller = "MembershipRole"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", info_model);
                }
                return View("Info", info_model);
            }
        }

        [HttpPost]
        public ActionResult Edit(MembershipRoleModel model)
        {
            membershipRoleRepository.EditObject(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            MembershipRoleModel model = new MembershipRoleModel();
            model.LoadCheckBoxList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("MembershipRole/_MembershipRoleAdd", model);
            }
            return View(model);
                
        }


        [HttpPost]
        public ActionResult Add(MembershipRoleModel model)
        {
            model.LoadCheckBoxList();
            if (ModelState.IsValid)
            {
                try
                {
                    membershipRoleRepository.AddObject(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania roli. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipRole/_MembershipRoleAdd", model);
                }
                return View(model);
            }



        }

        [HttpGet]
        public ActionResult Delete(string RoleName)
        {
            MembershipRoleModel role = membershipRoleRepository.Repository.FirstOrDefault(x => x.Name == RoleName);
            if (role != null)
            {
                DeleteObjectByName model = new DeleteObjectByName();
                model.Description = "Czy napewno chcesz usunąć role: " + role.Name + "?";
                model.Name = role.Name;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipRole/_MembershipRoleDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Rola nie istnieje",
                    Action = "Index",
                    Controller = "MembershipRole"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }


        [HttpPost]
        public ActionResult Delete(DeleteObjectByName model)
        {
            if (ModelState.IsValid)
            {
                MembershipRoleModel m = membershipRoleRepository.Repository.FirstOrDefault(x => x.Name == model.Name);
                membershipRoleRepository.DeleteObject(m);
                return RedirectToAction("Index");
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("MembershipRole/_MembershipRoleDelete", model);
                }
                return View("Delete", model);
            }
        }


        public JsonResult SortByList()
        {
            IQueryable list = membershipRoleListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}
