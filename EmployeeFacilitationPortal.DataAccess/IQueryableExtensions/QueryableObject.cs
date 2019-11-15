using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.DataRepository.IQueryableExtensions
{
    public class QueryableObject:IQueryObject
    {
        public string Search { get; set; }
        public string Order { get; set; }
        public string Orderby { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
