using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Grievance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      //  [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("GrievanceTypes")]
        public int TypeId { get; set; }
        public string Status { get; set; }

        public string EmployeeName { get; set; }
        public string GrievanceTitle { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public ICollection<GrievanceMessage>Messages { get; set; }


    }
}
