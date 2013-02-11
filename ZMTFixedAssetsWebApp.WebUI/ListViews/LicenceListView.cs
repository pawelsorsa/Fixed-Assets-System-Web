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
    public class LicenceListView : ListViewAsCollectionModel<Licence>
    {
        public LicenceListView(IRepository<Licence> licenceRepository)
            : base(licenceRepository)
        {
        }

        public override List<System.Web.Mvc.SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "id_number" });
            items.Add(new SelectListItem { Text = "Nr. inwentarzowy", Value = "inventory_number" });
            items.Add(new SelectListItem { Text = "Licencja", Value = "licence_number" });
            items.Add(new SelectListItem { Text = "Przypisany środek trwały", Value = "assign_fixed_asset" });
            items.Add(new SelectListItem { Text = "Utworzony przez", Value = "created_by" });
            items.Add(new SelectListItem { Text = "Data ost. modyfikacji", Value = "last_modified_date" });
            items.Add(new SelectListItem { Text = "Ostatnio edytowany przez", Value = "last_modified_login" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<Licence> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<Licence> repo, string sortby, bool asc, string query, bool search)
        {
            List<Licence> licenceList = new List<Licence>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            licenceList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id_number").ToList();

            if (QueryList.Count != 0)
            {
                string ID, InwNumber, Licence, AssignFixedAsset, CreatedBy, LastModifiedDate, LastModifiedLogin;
                ID = InwNumber = Licence = AssignFixedAsset = CreatedBy = LastModifiedDate = LastModifiedLogin = "";
                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("InwNumber", out InwNumber);
                QueryList.TryGetValue("Licence", out Licence);
                QueryList.TryGetValue("AssignFixedAsset", out AssignFixedAsset);
                QueryList.TryGetValue("CreatedBy", out CreatedBy);
                QueryList.TryGetValue("LastModifiedDate", out LastModifiedDate);
                QueryList.TryGetValue("LastModifiedLogin", out LastModifiedLogin);

                DateTime _LastModifiedDate;
                DateTime.TryParse(LastModifiedDate, out _LastModifiedDate);

                int _id, _asset;
                int.TryParse(ID, out _id);
                int.TryParse(AssignFixedAsset, out _asset);

                licenceList = licenceList.Where(x =>
                    (_id != 0 ? x.id_number == _id : x.id_number != 0) &&
                    (_asset != 0 ? x.assign_fixed_asset == _asset : x.assign_fixed_asset != 0) &&
                    (Licence != null ? x.licence_number == Licence : x.licence_number != "" || x.licence_number != null) &&
                    (CreatedBy != null ? x.created_by == CreatedBy : x.created_by != "" || x.created_by != null) &&
                    (LastModifiedLogin != null ? x.last_modified_login == LastModifiedLogin : x.last_modified_login != "" || x.last_modified_login != null) &&
                    (_LastModifiedDate != null ? x.last_modified_date.ToShortDateString() == _LastModifiedDate.ToShortDateString() : x.last_modified_date != null)
                    ).ToList();
            }

            int count = licenceList.Count();
            return new CountRecordsAndCreateListModel<Licence>() { List = licenceList.AsQueryable(), Count = count };
        
        }
    }
}