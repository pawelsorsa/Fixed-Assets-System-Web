using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Infrastructure;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using Ninject;

namespace ZMTFixedAssetsWebApp.WebUI.Validation
{
    public class FixedAssetIdValidation : ValidationAttribute
    {
        NinjectControllerFactory ninject = new NinjectControllerFactory();
        private IRepository<FixedAsset> repository;

        public FixedAssetIdValidation()
        {
            repository = ninject.Kernel.Get<IRepository<FixedAsset>>();
        }

        public override bool IsValid(object value)
        {
            int id;
            int.TryParse(value.ToString(), out id);
            if (id == 0) return true;

            bool result = repository.Repository.Any(x => x.id == id);
            return result;
        }
    }
}