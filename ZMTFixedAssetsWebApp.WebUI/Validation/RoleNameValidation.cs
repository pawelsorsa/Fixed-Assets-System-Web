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

    public class RoleNameValidation : ValidationAttribute
    {
        NinjectControllerFactory ninject = new NinjectControllerFactory();
        private IRepository<MembershipRoleModel> repository;
        
        public RoleNameValidation()
        {
            repository = ninject.Kernel.Get<IRepository<MembershipRoleModel>>();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            bool result = repository.Repository.Any(x => x.Name == value.ToString());
            return !result;
        }
    }
}