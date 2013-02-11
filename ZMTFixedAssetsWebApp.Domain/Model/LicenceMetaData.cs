using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    [MetadataType(typeof(LicenceMetaData))]
    public partial class Licence
    {
    }

    class LicenceMetaData
    {
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Pole ID musi być numerem.")]
        [Required(ErrorMessage = "Pole ID jest wymanage")]
        public int id_number { get; set; }

        [Required(ErrorMessage = "Pole numer inwentarzowy jest wymanage")]
        [StringLength(25, ErrorMessage = "Numer inwentarzowy powinien zawierać maksymalnie 25 znaków")]
        public string inventory_number { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Pole przypisany środek trwały musi być numerem.")]
        [Required(ErrorMessage = "Pole przypisany środek trwały jest wymanage")]
        public int assign_fixed_asset  { get; set;}


        [StringLength(50, ErrorMessage = "Licencja powinna zawierać maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Pole licencja jest wymanage")]
        public string licence_number { get; set; }

        [Required(ErrorMessage = "Pole nazwa jest wymanage")]
        [StringLength(50, ErrorMessage = "Nazwa powinna zawierać maksymalnie 50 znaków")]
        public string name { get; set;}

        [StringLength(250, ErrorMessage = "Komentarz powinien zawierać maksymalnie 50 znaków")]
        public string comment { get; set; }

        public string created_by { get; set; }

        public DateTime last_modified_date { get; set; }

        public string last_modified_login { get; set; }
    }
}
