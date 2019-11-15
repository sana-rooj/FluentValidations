using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class TrainingRequestType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
