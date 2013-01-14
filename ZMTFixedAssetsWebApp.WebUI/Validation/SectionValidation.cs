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
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.WebUI.Validation
{

    public class SectionValidation : ValidationAttribute
    {
        NinjectControllerFactory ninject = new NinjectControllerFactory();
        private IRepository<Section> repository;

        public SectionValidation()
        {
            repository = ninject.Kernel.Get<IRepository<Section>>();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string short_name = (string)value;
            return !repository.Repository.Any(x => x.short_name == short_name);
        }
    }
}