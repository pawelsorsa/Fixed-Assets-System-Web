using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;


namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public abstract class ListViewModel<T, ListType> : ListViewBase<T> where T: class, new () where ListType : class, new() 
    {

        public ListViewModel(IRepository<T> repo)
            : base(repo)
        {

        }
        
        protected abstract ListType CreateExtendedObejctModelFromObjectModel(T obj);

        public virtual PaginatedListModel<ListType> CreateListModel(int page, bool show_all, string sortby, int items_per_page, bool ASC, bool search, string query)
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
        
        public virtual IEnumerable<ListType> CreateExtendedListModelFromListModel(List<T> list)
        {
            List<ListType> temp = new List<ListType>();
            foreach (var item in list)
            {
                temp.Add((ListType)CreateExtendedObejctModelFromObjectModel(item));
            }
            return temp;
        }



    }
}