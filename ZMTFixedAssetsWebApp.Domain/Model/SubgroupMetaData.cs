using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    [MetadataType(typeof(SubgroupMetaData))]
    public partial class Subgroup
    {

    }

    class SubgroupMetaData
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Pole skrót nazwa jest wymanage")]
        [StringLength(10, ErrorMessage = "Skrót powien zawierać maksymalnie 10 znaków")]
        public string short_name { get; set; }
        [Required(ErrorMessage = "Pole nazwa jest wymanage")]
        [StringLength(50, ErrorMessage = "Nazwa powinna zawierać maksymalnie 50 znaków")]
        public string name { get; set; }
    }
}
