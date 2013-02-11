using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public sealed class SectionListView : ListViewAsCollectionModel<Section>
    {
        public SectionListView(IRepository<Section> sectionRepository)
            : base(sectionRepository)
        {
        }


        public override List<System.Web.Mvc.SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "Id" });
            items.Add(new SelectListItem { Text = "Skrót", Value = "short_name" });
            items.Add(new SelectListItem { Text = "Nazwa", Value = "name" });
            items.Add(new SelectListItem { Text = "Miejscowość", Value = "locality" });
            items.Add(new SelectListItem { Text = "Ulica", Value = "street" });
            items.Add(new SelectListItem { Text = "Poczta", Value = "post" });
            items.Add(new SelectListItem { Text = "Kod pocztowy", Value = "postal_code" });
            items.Add(new SelectListItem { Text = "Numer telefonu", Value = "phone_number" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<Section> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<Section> repo, string sortby, bool asc, string query, bool search)
        {
            List<Section> usersList = new List<Section>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            usersList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "Id").ToList();

            if (QueryList.Count != 0)
            {
                string ID, Short, Name, Locality, Street, Post, PostalCode, PhoneNumber;
                ID = Short = Name = Locality = Street = Post = PostalCode = PhoneNumber = "";


                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("Short", out Short);
                QueryList.TryGetValue("Name", out Name);
                QueryList.TryGetValue("Locality", out Locality);
                QueryList.TryGetValue("Street", out Street);
                QueryList.TryGetValue("Post", out Post);
                QueryList.TryGetValue("PostalCode", out PostalCode);
                QueryList.TryGetValue("PhoneNumber", out PhoneNumber);

                int _id;
                int.TryParse(ID, out _id);

                usersList = usersList.Where(x =>
                    (ID != null ? x.id == _id : x.id != 0) &&
                    (Short != null ? x.short_name == Short : x.short_name != "" || x.short_name != null) &&
                    (Name != null ? x.name == Name : x.name != "" || x.name != null) &&
                    (Locality != null ? x.locality == Locality : x.locality != "" || x.locality != null) &&
                    (Street != null ? x.street == Street : x.street != "" || x.street != null) &&
                    (Post != null ? x.post == Post : x.post != "" || x.post != null) &&
                    (PostalCode != null ? x.postal_code == PostalCode : x.postal_code != "" ||x.postal_code != null) &&
                    (PhoneNumber != null ? x.phone_number == PhoneNumber : x.phone_number != "" || x.phone_number != null)
                    ).ToList();
            }
            else
            {
                usersList = usersList.ToList();
            }
            int count = usersList.Count();
            return new CountRecordsAndCreateListModel<Section>() { List = usersList.AsQueryable(), Count = count };
        }
    }
}