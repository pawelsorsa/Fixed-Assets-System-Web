using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.LinqHelpers
{
    public static class LinqHelpers
    {
        public static IQueryable<T> OrderByFieldNullLast<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "x"); //(x)
            MemberExpression prop = null;

            PropertyInfo xxx = typeof(T).GetProperty(SortField);
            bool isnumeric;

            if (xxx != null)
            {
                isnumeric = HelperMethods.HelperMethods.IsNumeric(xxx.PropertyType);
                prop = Expression.Property(param, SortField); //(x.property) 
            }
            else
            {
                prop = Expression.Property(param, "id");//(x.property)
                isnumeric = true;
            }

            var exp = Expression.Lambda(prop, param); // x => x.property
            var body = Expression.Equal(Expression.PropertyOrField(param, SortField),
               isnumeric ? Expression.Constant(0) : Expression.Constant(null)); // x.parameter == 0 || x.parameter == null
            var lambda = Expression.Lambda<Func<T, bool>>(body, param); //x => x.parameter == null
            
        
            string method = !Ascending ? "OrderBy" : "OrderByDescending";
            MethodCallExpression orderByCallExpression = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { q.ElementType, lambda.Body.Type }, q.Expression, lambda);

            method = !Ascending ? "ThenBy" : "ThenByDescending";
            MethodCallExpression orderByCallExpression2 = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { q.ElementType, exp.Body.Type }, orderByCallExpression, exp);

                return q.Provider.CreateQuery<T>(orderByCallExpression2);
        }


        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "x"); //(x)
            MemberExpression prop = null;

            PropertyInfo xxx = typeof(T).GetProperty(SortField);

            if (xxx != null)
            {
                prop = Expression.Property(param, SortField); //(x.property)
            }
            else
            {
                prop = Expression.Property(param, "id"); //(x.id)
            }

            var exp = Expression.Lambda(prop, param); // (x => x.parameter)
            
            string method = !Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}