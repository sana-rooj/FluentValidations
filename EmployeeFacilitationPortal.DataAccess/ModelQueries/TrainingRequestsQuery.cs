using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;

namespace EmployeeFacilitationPortal.DataRepository.ModelQueries
{
    public class TrainingRequestsQuery : IQueryObject
    {
        public string Status { get; set; }
        public string Type { get; set; }
        public string EmployeeName { get; set; }
        public bool? ViewAll { get; set; }
        public int? EmployeeId { get; set; } 
        public string Search { get; set; }
        public string Order { get; set; }
        public string Orderby { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
