using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class ProfessionalReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string OrganizationName { get; set; }
        [MaxLength(100)]
        public string Designation { get; set; }
        [MaxLength(15)]
        public string ContactNumber { get; set; }
        [MaxLength(255)]
        public string EmailId { get; set; }
        [MaxLength(50)]
        public string Relationship { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        //public Employee Employee { get; set; }

    }
}
