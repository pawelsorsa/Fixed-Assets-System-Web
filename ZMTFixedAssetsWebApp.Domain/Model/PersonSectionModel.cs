using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    public class PersonSectionModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string section_name { get; set; }
        public string email { get; set; }
        public int? area_code { get; set; }
        public int? phone_number { get; set; }
        public int? phone_number2 { get; set; }
    }
}
