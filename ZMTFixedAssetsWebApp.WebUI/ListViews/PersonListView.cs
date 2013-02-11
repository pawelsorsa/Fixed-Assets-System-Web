using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Repositories;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.Controllers;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public sealed class PersonListView : ListViewAsCollectionModel<Person>
    {
        private SectionController sectionController;
        
        public PersonListView(IRepository<Person> personRepository, SectionController sectionController)
            : base(personRepository)
        {
            this.sectionController = sectionController;
        }

        protected override CountRecordsAndCreateListModel<Person> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<Person> repo, string sortby, bool asc, string query, bool search)
        {
            List<Person> personList = new List<Person>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            personList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id").ToList();
            if (QueryList.Count != 0)
            {
                string id, name, surname, email, phone, mobile, section;
                id = name = surname = email = phone = mobile = section = "";
                QueryList.TryGetValue("ID", out id);
                QueryList.TryGetValue("Name", out name);
                QueryList.TryGetValue("Surname", out surname);
                QueryList.TryGetValue("Section", out section);
                QueryList.TryGetValue("Email", out email);
                QueryList.TryGetValue("Phone", out phone);
                QueryList.TryGetValue("Mobile", out mobile);

                int _id, _phone, _mobile;
                int.TryParse(id, out _id);
                int.TryParse(phone, out _phone);
                int.TryParse(mobile, out _mobile);


                personList = personList.Where(x =>
                    (id != null ? x.id == _id : x.id != 0) &&
                    (name != null ? x.name == name.ToUpper() : x.name != "") &&
                    (surname != null ? x.surname == surname.ToUpper() : x.surname != "" || x.surname != null) &&
                    (email != null ? x.email == email : x.email != "" || x.email != null) &&
                    (section != null ? x.Section.short_name == section : x.Section.short_name != null) &&
                    (phone != null ? x.phone_number == _phone : x.phone_number != 0 || x.phone_number != null) &&
                    (mobile != null ? x.phone_number2 == _mobile : x.phone_number2 != 0 || x.phone_number2 != null)
                    ).ToList();
            }
            int count = personList.Count();
            return new CountRecordsAndCreateListModel<Person>() { List = personList.AsQueryable(), Count = count };
        }


        public override List<SelectListItem> OrderByList()
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

    }
}