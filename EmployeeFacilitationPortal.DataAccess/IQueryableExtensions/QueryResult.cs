using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.DataRepository.IQueryableExtensions
{
    public class QueryResult<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}
