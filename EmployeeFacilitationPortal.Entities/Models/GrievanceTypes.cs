using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class GrievanceTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }

        public int AssignedTo { get; set; }
    }
}
