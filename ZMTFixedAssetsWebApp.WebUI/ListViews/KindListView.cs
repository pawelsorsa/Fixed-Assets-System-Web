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
    public class KindListView : ListViewAsCollectionModel<Kind>
    {
        public KindListView(IRepository<Kind> kindRepository)
            : base(kindRepository)
        {
        }

        public override List<System.Web.Mvc.SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "id" });
            items.Add(new SelectListItem { Text = "Nazwa", Value = "name" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<Kind> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<Kind> repo, string sortby, bool asc, string query, bool search)
        {
            List<Kind> kindList = new List<Kind>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            kindList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id").ToList();


            if (QueryList.Count != 0)
            {
                string ID, Name;
                ID = Name = "";
                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("Name", out Name);

                int _id;
                int.TryParse(ID, out _id);

                kindList = kindList.Where(x =>
                    (_id != 0 ? x.id == _id : x.id != 0) &&
                    (Name != null ? x.name == Name : x.name != null)
                    ).ToList();
            }

            int count = kindList.Count();
            return new CountRecordsAndCreateListModel<Kind>() { List = kindList.AsQueryable(), Count = count };
        
        }
    }
}