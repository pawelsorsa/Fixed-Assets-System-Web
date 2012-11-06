using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class PersonCountRecordsAndCreateListModel
    {
        public int Count { get; set; }
        public IQueryable<Person> List { get; set; }
    }
}