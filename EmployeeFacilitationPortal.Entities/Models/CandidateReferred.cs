using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class CandidateReferred
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Resume { get; set; } //File
        public float YearsOfExperience { get; set; }
        [MaxLength(50)]
        public string Relationship { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<ReferredTechnicalSkill> ReferredTechnicalSkills { get; set; }

        public ICollection<EmployeeCandidateReferred> EmployeeCandidatesReferred { get; set; }
    }
}
