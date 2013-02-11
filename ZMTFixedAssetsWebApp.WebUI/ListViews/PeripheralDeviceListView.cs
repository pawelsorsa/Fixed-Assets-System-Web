using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public class PeripheralDeviceListView : ListViewAsCollectionModel<PeripheralDevice>
    {
        public PeripheralDeviceListView(IRepository<PeripheralDevice> deviceRepository)
            : base(deviceRepository)
        {
        }

        public override List<System.Web.Mvc.SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "id" });
            items.Add(new SelectListItem { Text = "Nazwa", Value = "name" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<PeripheralDevice> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<PeripheralDevice> repo, string sortby, bool asc, string query, bool search)
        {
            List<PeripheralDevice> deviceList = new List<PeripheralDevice>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            deviceList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id").ToList();


            if (QueryList.Count != 0)
            {
                string ID, Name;
                ID = Name = "";
                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("Name", out Name);

                int _id;
                int.TryParse(ID, out _id);

                deviceList = deviceList.Where(x =>
                    (_id != 0 ? x.id == _id : x.id != 0) &&
                    (Name != null ? x.name == Name : x.name != null) 
                    ).ToList();
            }

            int count = deviceList.Count();
            return new CountRecordsAndCreateListModel<PeripheralDevice>() { List = deviceList.AsQueryable(), Count = count };
        
        }
    }
}