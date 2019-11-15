using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeFacilitationPortal.Entities.Common.Utility;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[ForeignKey("Employee")]
       // public int EmployeeId { get; set; }
        [NotMapped]
        [MaxLength(255)]
        public string Password { get; set; }

        public byte[] EncryptedPassword { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [EmailAddress]
        [MaxLength(255)] public string Email { get; set; }
        //public Employee Employee { get; set; }
         
    }
}
