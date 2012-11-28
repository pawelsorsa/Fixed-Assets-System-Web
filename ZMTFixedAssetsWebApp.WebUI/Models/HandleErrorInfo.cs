using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class HandleErrorInfo
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ErrorMessage { get; set; }
    }
}