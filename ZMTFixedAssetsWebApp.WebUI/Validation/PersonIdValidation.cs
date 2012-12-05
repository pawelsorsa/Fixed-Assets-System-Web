using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ZMTFixedAssetsWebApp.WebUI.Repositories;

namespace ZMTFixedAssetsWebApp.WebUI.Validation
{

    public class PersonIdValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var id = (int)value;

            PersonRepository repository = new PersonRepository();
            bool result = repository.People.Any(x => x.id == id);

            return !result;
        }
    }
}