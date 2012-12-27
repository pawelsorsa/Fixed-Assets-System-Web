using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;


namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public abstract class ListViewModel<T, ListType> where T : class, new () where ListType : class, new () 
    {
        protected IRepository<T> Repositry;

        public ListViewModel()
        {
        }

        public ListViewModel(IRepository<T> repo) :base()
        {
            Repositry = repo;
        }

        public abstract List<SelectListItem> OrderByList();
        protected abstract ListType CreateExtendedObejctModelFromObjectModel(T obj);
        protected abstract CountRecordsAndCreateListModel<T> CountRecordsAndCreateListModel(IRepository<T> repo, string sortby, bool asc, string query, bool search);
        
        protected virtual IEnumerable<ListType> CreateExtendedListModelFromListModel(List<T> list)
        {
            List<ListType> temp = new List<ListType>();
            foreach (var item in list)
            {
                temp.Add((ListType)CreateExtendedObejctModelFromObjectModel(item));
            }
            return temp;
        }


        public virtual PaginatedListModel<ListType> CreateExtendedListModel(int page, bool show_all, string sortby, int items_per_page, bool ASC, bool search, string query)
        {
            List<SelectListItem> _ItemsPerPageList = ItemsPerPageList();
            List<SelectListItem> _OrderByList = OrderByList();

            CountRecordsAndCreateListModel<T> _CountRecordsAndCreateListModel = CountRecordsAndCreateListModel(Repositry, sortby, ASC, query, search);
            if (!CheckIfItemsPerPageExist(items_per_page, _ItemsPerPageList)) { items_per_page = 10; }

            List<T> List = CreateList(_CountRecordsAndCreateListModel.List.ToList(), page, _CountRecordsAndCreateListModel.Count,
                items_per_page, show_all);



            PaginatedListModel<ListType> model = new PaginatedListModel<ListType>()
            {
                List = CreateExtendedListModelFromListModel(List),
                DropDownList = _OrderByList,
                ItemsPerPageList = _ItemsPerPageList,
                ASC = ASC,
                TotalRecords = _CountRecordsAndCreateListModel.Count,
                ShowAll = show_all,
                OrderBy = sortby,
                Page = page,
                ItemsPerPage = items_per_page,
                Query = query
            };

            return model;
        }


        public virtual PaginatedListModel<T> CreateListModel(int page, bool show_all, string sortby, int items_per_page, bool ASC, bool search, string query)
        {
            List<SelectListItem> _ItemsPerPageList = ItemsPerPageList();
            List<SelectListItem> _OrderByList = ItemsPerPageList();

            CountRecordsAndCreateListModel<T> _CountRecordsAndCreateListModel = CountRecordsAndCreateListModel(Repositry, sortby, ASC, query, search);
            if (!CheckIfItemsPerPageExist(items_per_page, _ItemsPerPageList)) { items_per_page = 10; }

            List<T> List = CreateList(_CountRecordsAndCreateListModel.List.ToList(), page, _CountRecordsAndCreateListModel.Count,
                items_per_page, show_all);


            PaginatedListModel<T> model = new PaginatedListModel<T>()
            {
                List = List,
                DropDownList = _OrderByList,
                ItemsPerPageList = _ItemsPerPageList,
                ASC = ASC,
                TotalRecords = _CountRecordsAndCreateListModel.Count,
                ShowAll = show_all,
                OrderBy = sortby,
                Page = page,
                ItemsPerPage = items_per_page,
                Query = query
            };
            return model;
        }

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
    }
}