using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; set; }

       
        //[ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsAlreadyRegistered { get; set; }
        public bool PasswordSetStatus { get; set; }

        public int ExpiryInDays { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
