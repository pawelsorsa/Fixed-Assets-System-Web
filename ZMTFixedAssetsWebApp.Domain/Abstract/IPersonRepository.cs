using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.Domain.Abstract
{
    public interface IPersonRepository
    {
        IQueryable<Person> People { get; }
        void AddPerson(Person person);
        void EditPerson(Person person);
        void DeletePerson(Person person);
        void SaveChanges();

    }
}
