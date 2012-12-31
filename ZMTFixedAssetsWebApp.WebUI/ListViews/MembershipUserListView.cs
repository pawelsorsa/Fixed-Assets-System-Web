using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using System.Web.Mvc;
using System.Web.Security;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{
    public sealed class MembershipUserListView : ListViewAsCollectionModel<MembershipUserModel>
    {

        public MembershipUserListView(IRepository<MembershipUserModel> userRepository)
            : base(userRepository)
        {
        }


        public override List<SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "UserName", Value = "UserName" });
            items.Add(new SelectListItem { Text = "Email", Value = "Email" });
            items.Add(new SelectListItem { Text = "CreationDate", Value = "CreationDate" });
            items.Add(new SelectListItem { Text = "LastLoginDate", Value = "LastLoginDate" });
            items.Add(new SelectListItem { Text = "LastActivityDate", Value = "LastActivityDate" });
            items.Add(new SelectListItem { Text = "LastLockoutDate", Value = "LastLockoutDate" });
            items.Add(new SelectListItem { Text = "IsOnline", Value = "IsOnline" });
            return items;
        }


        protected override CountRecordsAndCreateListModel<MembershipUserModel> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<MembershipUserModel> repo, string sortby, bool asc, string query, bool search)
        {
            List<MembershipUserModel> usersList = new List<MembershipUserModel>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            usersList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "UserName").ToList();

            if (QueryList.Count != 0)
            {
                
            }
            else
            {
                usersList = usersList.ToList();
            }
            int count = usersList.Count();
            return new CountRecordsAndCreateListModel<MembershipUserModel>() { List = usersList.AsQueryable(), Count = count };
        }
    }
}