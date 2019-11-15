using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class EmployeeCandidateReferred
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("CandidateReferred")]
        public int CandidateReferredId { get; set; }
        //public Employee Employee { get; set; }
        public CandidateReferred CandidateReferred { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

}
