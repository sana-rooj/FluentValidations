using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeFacilitationPortal.Entities.Common.Utility;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public PersonalInfo PersonalInfo { get; set; } //The basic information
        public int EmpId { get; set; }
        [MaxLength(400)] public string Username { get; set; }
        public DateTime DateOfJoining { get; set; }
        [MaxLength(100)] public string Designation { get; set; }
        [MaxLength(200)] public string ProjectAssigned { get; set; }
        [MaxLength(200)] public string Terminated { get; set; }
        public bool IsValidated { get; set; }
        public bool IsActive { get; set; }//account status
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Login Login { get; set; }
        public Role Role { get; set; }
        public string TrainingDuringEducation { get; set; }
        public ICollection<ProfessionalReference> ProfessionalReferences { get; set; }
        public ICollection<EducationalRecord> EducationalRecords { get; set; }
        public ICollection<Dependent> Dependents { get; set; }
        public ICollection<WorkHistory> WorkHistories { get; set; }
      //  public ICollection<Claim> Claims { get; set; }
        public ICollection<Grievance> Grievances { get; set; }
        public ICollection<EmployeeCandidateReferred> EmployeeCandidatesReferred { get; set; }
        public ICollection<TrainingRequest> TrainingRequests { get; set; }
        public ICollection<Letter> Letters { get; set; }
        public ICollection<Query> Queries { get; set; }
        public ICollection<ForgotEmail> ForgotEmails { get; set; }
        public ICollection<Certification> Certifications { get; set; }
        public string EmployeeTechnicalSkills { get; set; }
        public ICollection<LanguageSkill> LanguageSkills { get; set; }

        //======= Updated fields for employee basic information ====///
        public string RelativeWorkingInCiklum { get; set; }
        public string SpecialArrangementsRequired { get; set; }
        public string MisconductInPreviousJob { get; set; }
        public ICollection<BasicInfoAttachments> AllAttachments { get; set; }


    }
}