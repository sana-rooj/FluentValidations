using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Claim
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
        [MaxLength(30)]
        public string Category { get; set; }
        public string SupportingDocument { get; set; }
        //public Employee Employee { get; set; }

    }
}
