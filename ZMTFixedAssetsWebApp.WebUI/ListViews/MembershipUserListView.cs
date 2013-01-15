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
            items.Add(new SelectListItem { Text = "Nazwa użytkownika", Value = "UserName" });
            items.Add(new SelectListItem { Text = "Email", Value = "Email" });
            items.Add(new SelectListItem { Text = "Data utworzenia", Value = "CreationDate" });
            items.Add(new SelectListItem { Text = "Data ostatniego logowania", Value = "LastLoginDate" });
            return items;
        }


        protected override CountRecordsAndCreateListModel<MembershipUserModel> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<MembershipUserModel> repo, string sortby, bool asc, string query, bool search)
        {
            List<MembershipUserModel> usersList = new List<MembershipUserModel>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            usersList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "UserName").ToList();

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
                

                usersList = usersList.Where(x =>
                    (UserName != null ? x.UserName == UserName : x.UserName != "") &&
                    (Email != null ? x.Email == Email : x.Email != "") &&
                    (LastLoginDate != null ? x.LastLoginDate.ToShortDateString() == _LastLoginDate.ToShortDateString() : x.LastLoginDate != null) &&
                    (CreationDate != null ? x.CreationDate.ToShortDateString() == _CreationDate.ToShortDateString() : x.CreationDate != null)
                    ).ToList();
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