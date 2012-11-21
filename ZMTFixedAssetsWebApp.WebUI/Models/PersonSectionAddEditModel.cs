using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Model;


namespace ZMTFixedAssetsWebApp.Domain.Model
{
    public class PersonSectionAddEditModel : PersonSectionModel
    {
        public IEnumerable<SelectListItem> SectionList { get; set; }
    }
}
