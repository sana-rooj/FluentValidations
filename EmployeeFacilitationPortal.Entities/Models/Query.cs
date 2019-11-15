using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Employee Employee { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public ICollection<QueryResponse> QueryResponses { get; set; }
    }
}
