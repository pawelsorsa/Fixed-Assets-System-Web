using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc.Ajax;
using ZMTFixedAssetsWebApp.WebUI.HtmlHelpers;


namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class InfoModel
    {
        public string Description { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }   
    }
}