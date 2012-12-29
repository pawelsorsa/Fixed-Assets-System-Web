using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public abstract class ListViewBase<T> where T : class, new()
    {
        protected IRepository<T> Repositry;
       
        public ListViewBase(IRepository<T> repo)
        {
            this.Repositry = repo;
        }
        
        public abstract List<SelectListItem> OrderByList();
        protected abstract CountRecordsAndCreateListModel<T> CountRecordsAndCreateListModel(IRepository<T> repo, string sortby, bool asc, string query, bool search);
       
 
        protected virtual List<T> CreateList(List<T> List, int Page, int CountPeople, int ItemsPerPage, bool ShowAll)
        {
            List<T> PersonList = new List<T>();
            Dictionary<string, string> QueryList = new Dictionary<string, string>();
            PersonList = List.Skip(ShowAll == true ? 0 : (Page - 1) * ItemsPerPage).Take(ShowAll == true ? CountPeople : ItemsPerPage).ToList();
            return PersonList;
        }


        protected virtual Dictionary<string, string> CreateQueryListDictionary(string query)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            string[] split = query.Split(',');
            foreach (var x in split)
            {
                string[] s = x.Split(':');

                if (s.Length > 1) { temp.Add(s[0], s[1]); }
            }
            return temp;
        }


        protected virtual List<SelectListItem> ItemsPerPageList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "40", Value = "40" });
            items.Add(new SelectListItem { Text = "60", Value = "60" });
            items.Add(new SelectListItem { Text = "80", Value = "80" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "150", Value = "150" });
            return items;
        }

        protected virtual bool CheckIfItemsPerPageExist(int ItemsPerPage, List<SelectListItem> ItemsPerPageList)
        {
            return ItemsPerPageList.Exists(x => x.Value == ItemsPerPage.ToString());
        }

        //
    }
}