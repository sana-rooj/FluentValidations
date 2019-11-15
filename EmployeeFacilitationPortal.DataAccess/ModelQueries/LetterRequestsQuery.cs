using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;

namespace EmployeeFacilitationPortal.DataRepository.ModelQueries
{
    public class LetterRequestsQuery:IQueryObject
    {
        public string Search { get; set; }
        public string Order { get; set; }
        public string Orderby { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

        public string Status { get; set; }
        public string LetterType { get; set; }
        public int? UserId { get; set; }

    }
}
