using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class ReferredTechnicalSkill
    {
        //Note: not using composite key, because that requires changes through Fluent API.
         [Key]
           public int Id { get; set; }

        [ForeignKey("TechnicalSkill")]
        public int TechnicalSkillId { get; set; }
       
        [ForeignKey("CandidateReferred")]
        public int CandidateReferredId { get; set; }
        public DateTime DateCreated { get; set; }
        public TechnicalSkill TechnicalSkill { get; set; }
        public CandidateReferred CandidateReferred { get; set; }
        public DateTime DateModified { get; set; }

    }
}
