using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class ForgotEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmailId { get; set; }

        [MaxLength(2048)]
        public string Link { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        //public Employee _Employee { get; set; }
    }
}
