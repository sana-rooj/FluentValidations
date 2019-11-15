using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class CompanyInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; } //for base64 and path of the file
        public string FileType { get; set; } //policy, handbook or orientation
        public string Filename { get; set; }
        public string WhatsNew { get; set; }
        public bool IsActive { get; set; }
    }
}