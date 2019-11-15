using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;

namespace EmployeeFacilitationPortal.DataRepository
{
    public class EmployeeQuery:IQueryObject
    {
        //FullName
        //    Designation
        //Roll
        public bool? IsValidated { get; set; }
        public bool? IsActive { get; set; }
        public int? EmpId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Designation { get; set; }
        public string Search { get; set; }
        public string Order { get; set; }//Order 
        public string Orderby { get; set; }//orderby=asc
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
