using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class DeleteObject
    {
        public bool Delete { get; set; }
        public string Description { get; set;}
        public int Id { get; set;  }
    }
}