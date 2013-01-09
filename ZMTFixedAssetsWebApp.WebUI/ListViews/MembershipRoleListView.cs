using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Models;

using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public class MembershipRoleListView : ListViewAsCollectionModel<MembershipRoleModel>
    {
        public MembershipRoleListView(IRepository<MembershipRoleModel> roleRepository)
            : base(roleRepository)
        {
        }


        public override List<SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Nazwa roli", Value = "Name" });
            return items;
        }


        protected override CountRecordsAndCreateListModel<MembershipRoleModel> CountRecordsAndCreateListModel(IRepository<MembershipRoleModel> repo, string sortby, bool asc, string query, bool search)
        {
            List<MembershipRoleModel> rolesList = new List<MembershipRoleModel>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            rolesList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "Name").ToList();

            if (QueryList.Count != 0)
            {
                string UserName, RoleName;

                UserName = RoleName = "";
                QueryList.TryGetValue("Name", out RoleName);
                
                rolesList = rolesList.Where(x =>
                    (RoleName != null ? x.Name == RoleName : x.Name != "")).ToList();
            }
            else
            {
                rolesList = rolesList.ToList();
            }
            int count = rolesList.Count();
            return new CountRecordsAndCreateListModel<MembershipRoleModel>() { List = rolesList.AsQueryable(), Count = count };
        }
    }
}