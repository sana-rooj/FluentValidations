using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class TrainingRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        public string DeliveryType { get; set; } // physical or virtual training
        public int TrainingRequestTypeId { get; set; } // technical or soft skills
        public string TrainingRequestTypeTitle { get; set; }
        
        public string BussinessJustification { get; set; } 

        public string Detail { get; set; } // maps to additional information field in APP
        [MaxLength(50)]
        public string Status { get; set; }
        public int Cost { get; set; }
        [NotMapped]
        public int TotalRecordsCountAccordingToCurrentPageLimit { get; set; } // used in frontend list view to display numbers in nav bar
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        //public Employee Employee { get; set; }
    } 

}
