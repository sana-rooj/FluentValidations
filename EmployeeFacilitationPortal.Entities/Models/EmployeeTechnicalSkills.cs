using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class EmployeeTechnicalSkills
    {
        public int Id { get; set; }
        [ForeignKey("TechnicalSkill")]
        public int TechnicalSkillId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public TechnicalSkill TechnicalSkill { get; set; }
        public Employee Employee { get; set; }
        
    }
}
