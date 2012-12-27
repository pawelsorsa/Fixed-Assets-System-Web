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
    public sealed class MembershipUserListView : ListViewModel<MembershipUser, MembershipUser>
    {

        public MembershipUserListView(IRepository<MembershipUser> userRepository)
            : base(userRepository)
        {
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

        protected override MembershipUser CreateExtendedObejctModelFromObjectModel(MembershipUser obj)
        {
            throw new NotImplementedException();
        }

        protected override CountRecordsAndCreateListModel<MembershipUser> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<MembershipUser> repo, string sortby, bool asc, string query, bool search)
        {
            List<MembershipUser> usersList = new List<MembershipUser>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            usersList = Repositry.Repository.OrderByFieldNullLast(sortby, asc).ToList();
            int id_section = 0;
            if (QueryList.Count != 0)
            {
            }
            else
            {
                usersList = usersList.ToList();
            }
            int count = usersList.Count();
            return new CountRecordsAndCreateListModel<MembershipUser>() { List = usersList.AsQueryable(), Count = count };
        }
    }
}