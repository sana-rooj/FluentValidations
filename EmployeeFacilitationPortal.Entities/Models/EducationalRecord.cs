using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class EducationalRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(100)]
        public string Degree { get; set; }
        [MaxLength(100)]
        public string Study { get; set; }
        [MaxLength(100)]
        public string InstitutionName { get; set; }
        public float Grade { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
        //public Employee Employee { get; set; }
    }
}
