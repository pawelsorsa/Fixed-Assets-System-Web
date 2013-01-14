using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using ZMTFixedAssetsWebApp.WebUI.Validation;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class SectionModel
    {
        [Required(ErrorMessage = "Pole ID nazwa jest wymanage")]
        public int id { get; set; }

        [SectionValidation(ErrorMessage = "Skrócona nazwa istnieje. Proszę wybrać inną")]
        [Required(ErrorMessage = "Pole skrócona nazwa jest wymanage")]
        [StringLength(10, ErrorMessage = "Skrócona nazwa powinna zawierać maksymalnie 10 znaków")]
        public string short_name { get; set; }

        [Required(ErrorMessage = "Pole nazwa nazwa jest wymanage")]
        [StringLength(50, ErrorMessage = "Nazwa powinna zawierać maksymalnie 50 znaków")]
        public string name { get; set; }

        [Required(ErrorMessage = "Pole adres jest wymanage")]
        [StringLength(50, ErrorMessage = "Adres powinna zawierać maksymalnie 50 znaków")]
        public string street { get; set; }

        [Required(ErrorMessage = "Pole kod pocztowy jest wymanage")]
        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "Proszę wpisać prawidłowy kod pocztowy")]
        public string postal_code { get; set; }

        [Required(ErrorMessage = "Pole email jest wymanage")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Proszę wpisać prawidłowy adres email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Pole miejscowość jest wymanage")]
        [StringLength(50, ErrorMessage = "Miejscowość powinna zawierać maksymalnie 50 znaków")]
        public string locality { get; set; }

        [Required(ErrorMessage = "Pole poczta jest wymanage")]
        [StringLength(50, ErrorMessage = "Poczta powinna zawierać maksymalnie 50 znaków")]
        public string post { get; set; }

        [Required(ErrorMessage = "Pole numer telefonu jest wymanage")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Nr telefonu musi być numerem.")]
        [Range(100000000, 999999999, ErrorMessage = "Nr telefonu jest nieprawidłowy")]
        public string phone_number { get; set; }
    }
}