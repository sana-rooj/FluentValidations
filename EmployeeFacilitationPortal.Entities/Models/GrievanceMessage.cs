using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class GrievanceMessage
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Grievance")]
        public int GrievanceId { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
