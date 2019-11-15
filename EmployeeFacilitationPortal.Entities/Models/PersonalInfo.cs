using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class PersonalInfo
    {
        private PersonalInfo personalInfo;

        public PersonalInfo(PersonalInfo personalInfo)
        {
            this.personalInfo = personalInfo;
        }

        public PersonalInfo(int id)
        {
            EmployeeId = id;
            Email = "Account"+id+"@gmail.com";
            Ntn = "8234567";
            Relative = "None";
            Disability = "None";
            FullName = "Not Entered";
            FatherName = "";
            HusbandName = "";
            Picture = "";
            DateOfBirth = DateTime.Now;
            Gender = "Male";
            MaritalStatus = "Married";
            Cnic = "71179-3409123-1";
            PassportNumber = "";
            PassportValidityDate = System.DateTime.Now;
            BloodGroup = "O+";
            PresentAddress = "";
            PermanentAddress = "";
            PersonalContactNumber = "";
            EmergencyContactNumber = "";
            EmergencyContactPerson = "";
            DateCreated = System.DateTime.Now;
            DateModified = System.DateTime.Now;

        }

        public PersonalInfo()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        //public Employee _Employee { get; set; } //One-to-One with employee

        [MaxLength(255)] public string Email { get; set; }
        [MaxLength(20)] public string Ntn { get; set; }
        [MaxLength(40)] public string Relative { get; set; }
        [MaxLength(200)] public string Disability { get; set; }
        [MaxLength(100)] public string FullName { get; set; }
        [MaxLength(100)] public string FatherName { get; set; }
        [MaxLength(100)] public string HusbandName { get; set; }
        public string Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(15)] public string Gender { get; set; }
        [MaxLength(20)] public string MaritalStatus { get; set; }
        [MaxLength(15)] public string Cnic { get; set; }
        [MaxLength(10)] public string PassportNumber { get; set; }
        public DateTime PassportValidityDate { get; set; }
        [MaxLength(3)] public string BloodGroup { get; set; }
        [MaxLength(400)] public string PresentAddress { get; set; }
        [MaxLength(400)] public string PermanentAddress { get; set; }
        [MaxLength(15)] public string PersonalContactNumber { get; set; }
        [MaxLength(15)] public string EmergencyContactNumber { get; set; }
        [MaxLength(100)] public string EmergencyContactPerson { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
