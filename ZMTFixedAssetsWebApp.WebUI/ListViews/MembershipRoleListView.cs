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
            items.Add(new SelectListItem { Text = "Nazwa użytkownika", Value = "UserName" });
            items.Add(new SelectListItem { Text = "Email", Value = "Email" });
            items.Add(new SelectListItem { Text = "Data utworzenia", Value = "CreationDate" });
            items.Add(new SelectListItem { Text = "Data ostatniego logowania", Value = "LastLoginDate" });
            return items;
        }


        protected override CountRecordsAndCreateListModel<MembershipRoleModel> CountRecordsAndCreateListModel(IRepository<MembershipRoleModel> repo, string sortby, bool asc, string query, bool search)
        {
            List<MembershipRoleModel> rolesList = new List<MembershipRoleModel>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            rolesList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "Name").ToList();

            if (QueryList.Count != 0)
            {
                string UserName, Email, CreationDate, LastLoginDate;

                UserName = Email = CreationDate = LastLoginDate = "";
                QueryList.TryGetValue("UserName", out UserName);
                QueryList.TryGetValue("Email", out Email);
                QueryList.TryGetValue("CreationDate", out CreationDate);
                QueryList.TryGetValue("LastLoginDate", out LastLoginDate);

                DateTime _LastLoginDate;
                DateTime.TryParse(LastLoginDate, out _LastLoginDate);
                DateTime _CreationDate;
                DateTime.TryParse(CreationDate, out _CreationDate);


                rolesList = rolesList.Where(x =>  x.Name == UserName).ToList();
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