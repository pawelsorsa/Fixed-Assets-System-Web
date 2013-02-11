using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.WebUI.Validation;
using System.ComponentModel.DataAnnotations;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class DeviceExtendedModel
    {
       public int id { get; set;}
       public int peripheral_device_id { get; set; }
       [Required]
       public string peripheral_device_name { get; set; }

       [StringLength(25, ErrorMessage = "Numer seryjny powinien zawierać maksymalnie 25 znaków")]
       public string serial_number { get; set; }

       [RegularExpression(@"^((([0-9]{1,2})|(1[0-9]{2,2})|(2[0-4][0-9])|(25[0-5])|\*)\.){3}(([0-9]{1,2})|(1[0-9]{2,2})|(2[0-4][0-9])|(25[0-5])|\*)$", ErrorMessage = "Proszę wpisać prawidłowy adres IP")]
       public string ip_address { get; set; }

       [RegularExpression(@"^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$", ErrorMessage = "Proszę wpisać prawidłowy adres MAC")]       
       public string mac_address { get; set; }

       [StringLength(25, ErrorMessage = "Producent powinien zawierać maksymalnie 25 znaków")]
       public string producer { get; set; }

       [StringLength(25, ErrorMessage = "Model powinien zawierać maksymalnie 25 znaków")]
       public string modell { get; set; }
       
       [StringLength(255, ErrorMessage = "Komentarz powinien zawierać maksymalnie 255 znaków")]
       public string comment { get; set; }

       [Required(ErrorMessage = "Pole ID środka trwałego jest wymanage")]
       [FixedAssetIdValidation(ErrorMessage = "Środek trwały o podanym ID nie istnieje")]
       public int id_fixed_asset { get; set; }
       
       public IEnumerable<SelectListItem> SectionList { get; set; }
    }
}