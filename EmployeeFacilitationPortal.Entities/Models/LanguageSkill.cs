using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class LanguageSkill
    {
        [Key]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public string Language { get; set; }

        // Superior or Advanced or Inrtermediate or Novice
        public string ProficiencyLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
