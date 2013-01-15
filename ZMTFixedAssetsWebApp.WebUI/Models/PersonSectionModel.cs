using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ZMTFixedAssetsWebApp.WebUI.Validation;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class PersonSectionModel
    {
        [PersonIdValidation(ErrorMessage= "Pracownik o podanym ID istnieje")]
        [Required(ErrorMessage = "Pole ID jest wymanage")]
        [Range(1, 999999999, ErrorMessage = "ID powinno być liczbą")]
        public int id { get; set; }

        [Required(ErrorMessage="Pole imię jest wymanage")]
        [StringLength(20, ErrorMessage = "Imię powinno zawierać maksymalnie 20 znaków")]
        public string name { get; set; }

        [Required(ErrorMessage = "Pole nazwisko jest wymanage")]
        [StringLength(20, ErrorMessage = "Nazwisko powinno zawierać maksymalnie 20 znaków")]
        public string surname { get; set; }
        
        public string section_name { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Proszę wpisać prawidłowy adres email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Pole numer kierunkowy jest wymanage")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Nr kierunkowy musi być numerem")]
        [Range(10,99, ErrorMessage="Nr kierunkowy jest niepoprawny")]
        public int? area_code { get; set; }

        [Required(ErrorMessage = "Pole numer telefonu jest wymanage")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Nr telefonu musi być numerem.")]
        [Range(1000000, 9999999, ErrorMessage="Nr telefonu jest nieprawidłowy")]
        public int? phone_number { get; set; }


        [Range(100000000, 999999999, ErrorMessage= "Nr telefonu komórkowego jest nieprawidłowy")]
        public int? phone_number2 { get; set; }
    }
}
