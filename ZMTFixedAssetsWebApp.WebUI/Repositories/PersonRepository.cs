using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Person> People
        {
            get { return context.Persons; }
        }
    }
}