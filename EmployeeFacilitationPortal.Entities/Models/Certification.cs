using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Certification
    {
        [Key]
        public int Id { get; set; }
 
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public string CertificateScannedCopy { get; set; }
        public string CertificateName { set; get; }
        public string CertificationLink { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

      
    }
}
