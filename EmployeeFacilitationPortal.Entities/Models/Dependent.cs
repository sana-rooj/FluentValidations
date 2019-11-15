using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Dependent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(15)]
        public string Cnic { get; set; }
        [MaxLength(50)]
        public string Relationship { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        //public Employee Employee { get; set; }
    }
}
