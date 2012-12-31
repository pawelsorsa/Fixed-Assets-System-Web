using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class DeleteObjectByName
    {
        public bool Delete { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}