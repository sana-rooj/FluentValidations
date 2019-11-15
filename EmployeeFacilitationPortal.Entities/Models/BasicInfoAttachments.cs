using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class BasicInfoAttachments
    {
        [Key]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentContents { get; set; }
    }
}
