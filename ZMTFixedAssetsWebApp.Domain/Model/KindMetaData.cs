using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    [MetadataType(typeof(KindMetaData))]
    public partial class Kind
    {
    }

    class KindMetaData
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Pole nazwa jest wymanage")]
        [StringLength(50, ErrorMessage = "Nazwa powinna zawierać maksymalnie 50 znaków")]
        public string name { get; set; }
    }
}
