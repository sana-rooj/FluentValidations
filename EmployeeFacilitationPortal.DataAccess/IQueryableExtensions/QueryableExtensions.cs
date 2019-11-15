using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace EmployeeFacilitationPortal.DataRepository.IQueryableExtensions
{
    public class QueryableExtensions<TEntity> : IQueryableExtensions<TEntity> where TEntity : class
    {

        public IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, IQueryObject queryObj)
        {
            if (string.IsNullOrWhiteSpace(queryObj.Orderby) && string.IsNullOrWhiteSpace(queryObj.Order) || !(IsPresent(queryObj.Order)))
                return query;

            if (queryObj.Orderby.Equals("asc"))
                return query.OrderBy(p => EF.Property<object>(p, queryObj.Order));
            return query.OrderByDescending(p => EF.Property<object>(p, queryObj.Order));
        }
        public IQueryable<TEntity> ApplyOrderingColumnMap(IQueryable<TEntity> query, IQueryObject queryObj, Dictionary<string, Expression<Func<TEntity, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.Orderby) && string.IsNullOrWhiteSpace(queryObj.Order) || !columnsMap.ContainsKey(queryObj.Order))
                return query;

            if (queryObj.Orderby.Equals("asc"))
                return query.OrderBy(columnsMap[queryObj.Order]);
            return query.OrderByDescending(columnsMap[queryObj.Order]);
        }

        public IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.Limit <= 0)
                queryObj.Limit = 10;

            return query.Skip((queryObj.Page - 1) * queryObj.Limit).Take(queryObj.Limit);
        }

        private List<string> GetPropertiesNameOfClass()
        {
            var props = typeof(TEntity).GetProperties();
            List<string> propertiesNamesList = new List<string>();
            foreach (var prop in props)
            {
                propertiesNamesList.Add(prop.Name);
            }

            return propertiesNamesList;
        }

        private bool IsPresent(string sortBy)
        {
            return this.GetPropertiesNameOfClass().Contains(sortBy);
        }
    }
}