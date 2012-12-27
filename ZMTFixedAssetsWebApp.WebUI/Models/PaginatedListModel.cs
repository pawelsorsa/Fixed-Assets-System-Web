using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class PaginatedListModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public IEnumerable<SelectListItem> DropDownList { get; set; }
        public IEnumerable<SelectListItem> ItemsPerPageList { get; set; }
        public string OrderBy { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public bool ShowAll { get; set; }
        public bool ASC { get; set; }
        public string Query { get; set; }
    }
}