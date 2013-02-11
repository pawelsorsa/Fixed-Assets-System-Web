using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public class SubgroupListView : ListViewAsCollectionModel<Subgroup>
    {
        public SubgroupListView(IRepository<Subgroup> subgroupRepository)
            : base(subgroupRepository)
        {
        }

        public override List<SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "id" });
            items.Add(new SelectListItem { Text = "Skrót", Value = "short_name" });
            items.Add(new SelectListItem { Text = "Nazwa", Value = "name" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<Subgroup> CountRecordsAndCreateListModel(IRepository<Subgroup> repo, string sortby, bool asc, string query, bool search)
        {
            List<Subgroup> subgroupList = new List<Subgroup>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            subgroupList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id").ToList();


            if (QueryList.Count != 0)
            {
                string ID, Name, Short;
                ID = Name = "";
                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("Name", out Name);
                QueryList.TryGetValue("Short", out Short);

                int _id;
                int.TryParse(ID, out _id);

                subgroupList = subgroupList.Where(x =>
                    (_id != 0 ? x.id == _id : x.id != 0) &&
                    (Name != null ? x.name == Name : x.name != "" || x.name != null) &&
                    (Short != null ? x.short_name == Short : x.short_name != "" || x.short_name != null)
                    ).ToList();
            }

            int count = subgroupList.Count();
            return new CountRecordsAndCreateListModel<Subgroup>() { List = subgroupList.AsQueryable(), Count = count };
        
        }
    }
}