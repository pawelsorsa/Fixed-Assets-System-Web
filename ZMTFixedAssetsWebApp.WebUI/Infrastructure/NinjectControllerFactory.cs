using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.Repositories;
using System.Web.Security;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IRepository<Section>>().To<SectionRepository>();
            ninjectKernel.Bind<IRepository<Person>>().To<PersonRepository>();
            ninjectKernel.Bind<IRepository<MembershipUserModel>>().To<MembershipUserRepository>();
            ninjectKernel.Bind<IRepository<MembershipRoleModel>>().To<MembershipRoleRepository>();
        }

        public IKernel Kernel
        {
            get { return ninjectKernel; }
        }
    }
}