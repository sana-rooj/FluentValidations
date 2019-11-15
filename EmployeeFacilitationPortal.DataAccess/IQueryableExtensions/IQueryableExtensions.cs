using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.DataRepository.IQueryableExtensions
{
    public interface IQueryableExtensions<TEntity> where TEntity : class
    {
        IQueryable<TEntity> ApplyOrderingColumnMap(IQueryable<TEntity> query, IQueryObject queryObj,
            Dictionary<string, Expression<Func<TEntity, object>>> columnsMap);
        IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, IQueryObject queryObj);
        IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, IQueryObject queryObj);
    }
}
