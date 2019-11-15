using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.DataRepository.IQueryableExtensions
{
    public interface IQueryObject
    {
        string Search { get; set; }
        string Order { get; set; }//column name for sorting
        string Orderby { get; set; }//sort Order
        int Page { get; set; }//page number
        int Limit { get; set; }//limit of rows per page
    }
}
