using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ZMTFixedAssetsWebApp.WebUI.Repositories;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using Ninject;
using ZMTFixedAssetsWebApp.WebUI.Infrastructure;

namespace ZMTFixedAssetsWebApp.WebUI.Validation
{

    public class PersonIdValidation : ValidationAttribute
    {
        NinjectControllerFactory ninject = new NinjectControllerFactory();
        private IRepository<Person> repository;

        public PersonIdValidation()
        {
            repository = ninject.Kernel.Get<IRepository<Person>>();
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var id = (int)value;

            bool result = repository.Repository.Any(x => x.id == id);
            return !result;
        }
    }
}