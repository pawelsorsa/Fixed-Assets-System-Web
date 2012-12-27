using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class CountRecordsAndCreateListModel<T>
    {
        public int Count { get; set; }
        public IQueryable<T> List { get; set; }
    }
}