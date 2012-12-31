using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMTFixedAssetsWebApp.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Repository { get; }
        void AddObject(T obj);
        void DeleteObject(T obj);
        void EditObject(T obj);
        int SaveChanges();
    }
}
