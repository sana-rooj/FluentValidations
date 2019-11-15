using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class TechnicalSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<ReferredTechnicalSkill> ReferredTechnicalSkills { get; set; }
        public ICollection<EmployeeTechnicalSkills> EmployeeTechnicalSkills { get; set; }
    }

}
