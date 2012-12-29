using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public abstract class ListViewAsCollectionModel<T> : ListViewBase<T> where T : class, new()
    {
        public ListViewAsCollectionModel(IRepository<T> repo)
            : base(repo)
        {
            Repositry = repo;
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



    }
}