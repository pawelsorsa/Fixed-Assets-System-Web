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


namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository personRepository;
        private ISectionRepository sectionRepository;

        public PersonController(IPersonRepository personRepository, ISectionRepository sectionRepository)
        {
            this.personRepository = personRepository;
            this.sectionRepository = sectionRepository;
        }
        
        [HttpGet]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Person person = personRepository.People.FirstOrDefault(x => x.id == id);
            PersonSectionAddEditModel model = CreatePersonSetionAddEditFromPerson(person, SectionsDictionary());
    
            return View("Edit", model);
        }
       


        [HttpPost]
        [HandleError]
        public ActionResult Edit(PersonSectionAddEditModel model)
        {
            if (ModelState.IsValid)
            {
                Person person = personRepository.People.FirstOrDefault(x => x.id == model.id);
                UpdatePerson(ref person, model, SectionsDictionary());
                if (person != null)
                {
                    personRepository.SavePerson(person);
                    
                    return RedirectToAction("List");
                }
                else
                {
                    return View("Error2");
                }
            }
            else
            {
                return View();
            }
        }



        public JsonResult SortByList()
        {
            IQueryable list = OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search()
        {
            
            return View("Index");
        }


        [HttpGet]
        public ActionResult List(string Section = "", int Page = 1, bool ShowAll = false, string OrderBy = "name", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query="")
        {
            PersonListModel model = CreatePersonListView(Section, Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query);
           
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonList", model);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult List(PersonListModel model, FormCollection collection)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        private PersonListModel CreatePersonListView(string section, int page, bool show_all, string sortby, int items_per_page, bool ASC, bool search, string query)
        {
            List<SelectListItem> _ItemsPerPageList = ItemsPerPageList();
            List<SelectListItem> _OrderByList = OrderByList();
            Dictionary<int, string> sectionDictionary = SectionsDictionary();
            PersonCountRecordsAndCreateListModel personCountListModel = PersonList_Count(personRepository, section, sortby, ASC, query, search);
            if (!CheckIfItemsPerPageExist(items_per_page, _ItemsPerPageList)) { items_per_page = 10; }
            List<Person> personList = CreatePersonList(personCountListModel.List.ToList(), page, personCountListModel.Count,
                items_per_page, show_all);

            PersonListModel model = new PersonListModel()
            {
                List = CreatePersonSetionModelListFromPersonList(personList, sectionDictionary),
                DropDownList = _OrderByList,
                ItemsPerPageList = _ItemsPerPageList,
                ASC = ASC,
                TotalRecords = personCountListModel.Count,
                ShowAll = show_all,
                OrderBy = sortby,
                Page = page,
                ItemsPerPage = items_per_page,
                Section = section,
                Query = query
            };
         
            return model;
        }

        private List<Person> CreatePersonList(List<Person> List, int Page, int CountPeople, int ItemsPerPage, bool ShowAll)
        {
            List<Person> PersonList = new List<Person>();
            Dictionary<string, string> QueryList = new Dictionary<string, string>();
            PersonList = List.Skip(ShowAll == true ? 0 : (Page - 1) * ItemsPerPage).Take(ShowAll == true ? CountPeople : ItemsPerPage).ToList();
            return PersonList;
        }

        
        private bool CheckIfItemsPerPageExist(int ItemsPerPage, List<SelectListItem> ItemsPerPageList)
        {
            return ItemsPerPageList.Exists(x => x.Value == ItemsPerPage.ToString());
        }

        private Dictionary<string, string> CreateQueryListDictionary(string query)
        {
            Dictionary<string, string> temp  = new Dictionary<string, string>();
            string[] split = query.Split(',');
            foreach (var x in split)
            {
                string[] s = x.Split(':');

                if (s.Length > 1) { temp.Add(s[0], s[1]); }
            }
            return temp;
        }


        private PersonCountRecordsAndCreateListModel PersonList_Count(IPersonRepository personRepository, string section, string sortby, bool asc, string query, bool search)
        {
            List<Person> personList = new List<Person>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            int id_section = GetSectionIdIfSectionExist(section);
            personList = personRepository.People.OrderByFieldNullLast(sortby, asc).ToList();

            if (QueryList.Count != 0)
            {
                string id, name, surname, email, phone, mobile;
                id = name = surname = email = phone = mobile = "";
                QueryList.TryGetValue("ID", out id);
                QueryList.TryGetValue("Name", out name);
                QueryList.TryGetValue("Surname", out surname);
                QueryList.TryGetValue("Section", out section);
                QueryList.TryGetValue("Email", out email);
                QueryList.TryGetValue("Phone", out phone);
                QueryList.TryGetValue("Mobile", out mobile);

                int _id, _phone, _mobile;
                id_section = GetSectionIdIfSectionExist(section != null ? section.ToUpper() : section);
                int.TryParse(id, out _id);
                int.TryParse(phone, out _phone);
                int.TryParse(mobile, out _mobile);


                personList = personList.Where(x =>
                    (id != null ? x.id == _id : x.id != 0) &&
                    (name != null ? x.name == name.ToUpper() : x.name != "") &&
                    (surname != null ? x.surname == surname.ToUpper() : x.surname != "") &&
                    (email != null ? x.email == email : x.email != "") &&
                    (section != null ? x.id_section == id_section : x.id_section != 0) && 
                    (phone != null ? x.phone_number == _phone : x.phone_number != 0) &&
                    (mobile != null ? x.phone_number2 == _mobile : x.phone_number2 != 0)
                    ).ToList();
            }
            else
            {
                personList = personList.Where(x => id_section == 0 ? true : x.id_section == id_section).ToList();
            }
            int count = personList.Count();
            return new PersonCountRecordsAndCreateListModel() { List = personList.AsQueryable(), Count = count };
        }


        private Dictionary<int, string> SectionsDictionary()
        {
            SectionController section_ctrl = new SectionController(sectionRepository);
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict = section_ctrl.GetAllShortNameSections();
            return dict;
        }

        IEnumerable<string> SectionNameList()
        {
            IEnumerable<string> list = sectionRepository.Sections.Select(x => x.short_name).AsEnumerable();
            return list;
        }


        private int GetSectionIdIfSectionExist(string section)
        {
            int id_section = 0;
            SectionController section_ctrl = new SectionController(sectionRepository);
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict = section_ctrl.GetAllShortNameSections();
            if (dict.ContainsValue(section))
            {
                id_section = dict.Where(x => x.Value == section).Select(x => x.Key).First();
            }
            return id_section;
        }

        private void UpdatePerson(ref Person person, PersonSectionAddEditModel personSection, Dictionary<int, string> sectionList)
        {
            if (personSection.area_code != null) person.area_code = personSection.area_code;
            if (personSection.email != null) person.email = personSection.email;
            if (personSection.name != null) person.name = personSection.name;
            if (personSection.phone_number != null) person.phone_number = personSection.phone_number;
            if (personSection.phone_number2 != null) person.phone_number2 = personSection.phone_number2;
            if (personSection.surname != null) person.surname = personSection.surname;
            int id = 0;
            int.TryParse(personSection.section_name, out id);
            
            
            person.id_section = id;
        }

        private Person CreatePersonFromPersonSectionModel(PersonSectionModel ps, Dictionary<int, string> sectionList)
        {
            Person temp = new Person();
            temp.id = ps.id;
            temp.area_code = ps.area_code;
            temp.email = ps.email;
            temp.id_section = sectionList.Where(x => x.Value == ps.section_name).Select(x => x.Key).First();
            temp.name = ps.name;
            temp.phone_number = ps.phone_number;
            temp.phone_number2 = ps.phone_number2;
            temp.surname = ps.surname;
            if (temp.id_section == 0) 
                return temp = null;
            else
                return temp;
        }



        private PersonSectionModel CreatePersonSetionModelFromPerson(Person person, Dictionary<int, string> sectionList)
        {
            PersonSectionModel temp = new PersonSectionModel();
            temp.area_code = person.area_code;
            temp.email = person.email;
            temp.id = person.id;
            temp.name = person.name;
            temp.phone_number = person.phone_number;
            temp.phone_number2 = person.phone_number2;
            temp.surname = person.surname;
            temp.section_name = sectionList.Where(x => x.Key == person.id_section).Select(x=>x.Value).First();
            return temp;
        }



        private PersonSectionAddEditModel CreatePersonSetionAddEditFromPerson(Person person, Dictionary<int, string> sectionList)
        {
            PersonSectionAddEditModel temp = new PersonSectionAddEditModel();
            temp.area_code = person.area_code;
            temp.email = person.email;
            temp.id = person.id;
            temp.name = person.name;
            temp.phone_number = person.phone_number;
            temp.phone_number2 = person.phone_number2;
            temp.surname = person.surname;
            temp.SectionList = SectionsShortNamesList();
            temp.section_name = sectionList.Where(x => x.Key == person.id_section).Select(x => x.Value).First();
            return temp;
        }

       
        private List<PersonSectionModel> CreatePersonSetionModelListFromPersonList(List<Person> list, Dictionary<int, string> sectionList)
        {
            List<PersonSectionModel> ps = new List<PersonSectionModel>();
            foreach (var person in list)
            {
                ps.Add((PersonSectionModel)CreatePersonSetionModelFromPerson(person, sectionList));
            }
            return ps;
        }

        private List<SelectListItem> SectionsShortNamesList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var x in SectionsDictionary())
            {
                items.Add(new SelectListItem { Text = x.Value, Value = x.Key.ToString() });
            }

            return items;
        }


        private List<SelectListItem> OrderByList()
        {     
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "ID" });
            items.Add(new SelectListItem { Text = "Imię", Value = "name" });
            items.Add(new SelectListItem { Text = "Nazwisko", Value = "surname" });
            items.Add(new SelectListItem { Text = "Sekcja", Value = "id_section" });
            items.Add(new SelectListItem { Text = "Email", Value = "email" });
            items.Add(new SelectListItem { Text = "Telefon", Value = "phone_number" });
            items.Add(new SelectListItem { Text = "Telefon kom.", Value = "phone_number2" });
            return items;
        }

        private List<SelectListItem> ItemsPerPageList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "40", Value = "40" });
            items.Add(new SelectListItem { Text = "60", Value = "60" });
            items.Add(new SelectListItem { Text = "80", Value = "80" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "150", Value = "150" });
            return items;
        }
    }
}
