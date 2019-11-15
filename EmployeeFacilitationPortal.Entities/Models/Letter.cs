using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Letter
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string Reason { get; set; }
        [MaxLength(500)]
        public string Response { get; set; }
        [MaxLength(30)]
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        //public Employee Employee { get; set; }

    }
}
