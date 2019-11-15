using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
   public class LetterRequests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("LetterTypes")]
        public int LetterTypeId { get; set; }

        [ForeignKey("Employee")]
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        
        public string Status { get; set; }
        public string Urgency { get; set; }
        public string Information { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public LetterTypes LetterType { get; set; } //The letter description



    }
}
