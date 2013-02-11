using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.Infrastructure;
using Ninject;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Repositories;
using ZMTFixedAssetsWebApp.WebUI.HtmlHelpers;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using System.Reflection;
using System.Data;
using ZMTFixedAssetsWebApp.WebUI.ListViews;



namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class PersonController : Controller
    {
        private IRepository<Person> personRepository;
        private IRepository<Section> sectionRepository;
        private PersonListView model;
        private SectionController section_ctrl;

        public PersonController(IRepository<Person> personRepository, IRepository<Section> sectionRepository)
        {
            this.personRepository = personRepository;
            this.sectionRepository = sectionRepository;
            section_ctrl = new SectionController(sectionRepository);
            model = new PersonListView(personRepository, section_ctrl);
        }
        

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Person/_PersonIndex", model.CreateListModel(1, false, "name", 10, false, false, ""));
            }
            return View(model.CreateListModel(1, false, "name", 10, false, false, ""));
        }

        [ZMTFixedAssetsWebApp.WebUI.ActionFilters.AccessDeniedAuthorize(Roles="Admins")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Person person = personRepository.Repository.FirstOrDefault(x => x.id == id);

            if (person != null)
            {

                PersonSectionAddEditModel model = CreatePersonSetionAddEditFromPerson(person);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Person/_PersonEdit", model);
                }
                return View("Edit", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podany pracownik nie istnieje",
                    Action = "Index",
                    Controller = "Person"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }
        

        [HttpPost]
        public ActionResult Edit(PersonSectionAddEditModel model)
        {
            ModelState.Remove("Id"); 
            if (ModelState.IsValid)
            {
                try
                {
                    Person person = personRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    UpdatePerson(ref person, model);
                    personRepository.EditObject(person);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw new Exception("Nie udało się edytować pracowanika. Proszę skontaktować się z administratorem");
                }               
            }
            else
            {
                model.SectionList = section_ctrl.SectionsShortNamesList();
                
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Person/_PersonEdit", model);
                }
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Person person = personRepository.Repository.FirstOrDefault(x => x.id == id);
            if (person != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć pracownika: " + person.id + " " + person.name + " " + person.surname + "?";
                model.Id = id;
                
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Person/_PersonDelete", model);
                }
                 
                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel() 
                { 
                    Description = "Podany pracownik nie istnieje",
                    Action = "Index", Controller = "Person"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }


       
        [HttpPost]
        public ActionResult Delete(DeleteObjectById model)
        {
            try
            {
                Person person = new Person() { id = model.Id };
                personRepository.DeleteObject(person);
                return RedirectToAction("Index");    
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania pracownika. Proszę skontaktować się z administratorem", ex.InnerException);
            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            PersonSectionAddEditModel model = new PersonSectionAddEditModel();
            model.SectionList = section_ctrl.SectionsShortNamesList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Person/_PersonAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PersonSectionAddEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Person person = CreatePersonFromPersonSectionAddEditModel(model);
                    personRepository.AddObject(person);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania pracownika. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                model.SectionList = section_ctrl.SectionsShortNamesList();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Person/_PersonAdd", model);
                }
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "name", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query="")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Person/_PersonList", model.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(model.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }


        [HttpPost]
        public ActionResult List(PaginatedListModel<PersonSectionModel> model, FormCollection collection)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        public JsonResult SortByList()
        {
            IQueryable list = model.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

 
        private void UpdatePerson(ref Person person, PersonSectionAddEditModel personSection)
        {
            if (personSection.id != 0) person.id = personSection.id; else person.id = 0;
            if (personSection.area_code != null) person.area_code = personSection.area_code; else person.area_code = null;
            if (personSection.email != null) person.email = personSection.email; else person.email = null;
            if (personSection.name != null) person.name = personSection.name.ToUpper(); else person.name = null;
            if (personSection.phone_number != null) person.phone_number = personSection.phone_number; else person.phone_number = null;
            if (personSection.phone_number2 != null) person.phone_number2 = personSection.phone_number2; else person.phone_number2 = null;
            if (personSection.surname != null) person.surname = personSection.surname.ToUpper(); else person.surname = null;
           // person.id_section = GetAllShortNameSections().Where(x => x.Value == personSection.section_name).Select(x => x.Key).First();
            int id = 0;
            int.TryParse(personSection.section_name, out id);
            person.id_section = id;
        }

  
        private Person CreatePersonFromPersonSectionAddEditModel(PersonSectionAddEditModel model)
        {
            Person person = new Person();
            person.area_code = model.area_code;
            person.email = model.email;
            person.id = model.id;
            int id_section;
            int.TryParse(model.section_name, out id_section);
            person.id_section = id_section;
            person.name = model.name;
            person.phone_number = model.phone_number;
            person.phone_number2 = model.phone_number2;
            person.surname = model.surname;

            return person;
        }


        private PersonSectionAddEditModel CreatePersonSetionAddEditFromPerson(Person person)
        {
            PersonSectionAddEditModel temp = new PersonSectionAddEditModel();
            temp.area_code = person.area_code;
            temp.email = person.email;
            temp.id = person.id;
            temp.name = person.name;
            temp.phone_number = person.phone_number;
            temp.phone_number2 = person.phone_number2;
            temp.surname = person.surname;
            // delete because @Html.DropDownListFor(x => x.section_name, @Model.SectionList) doesn't work properly
            //temp.section_name = section_ctrl.GetAllShortNameSections().Where(x => x.Key == person.id_section).Select(x => x.Value).First();
            temp.SectionList = section_ctrl.SectionsShortNamesList();
            var p = temp.SectionList.FirstOrDefault(x => x.Value == person.id_section.ToString());
            p.Selected = true;
            return temp;
        }
    }
}
