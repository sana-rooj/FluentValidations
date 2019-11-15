using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class WorkHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string OrganizationName { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        [MaxLength(1000)]
        public string ReasonForLeaving { get; set; }
        [MaxLength(500)]
        public string ToolsAndTechnologies { get; set; }
        public int LastDrawnSalary { get; set; }
        [MaxLength(100)]
        public string Benefit { get; set; }
        [MaxLength(100)]
        public string Designation { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ProjectDescriptionAndResponsibilities { get; set; }
        //public Employee Employee { get; set; }

    }
}
