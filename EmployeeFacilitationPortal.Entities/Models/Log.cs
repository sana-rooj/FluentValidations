using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } //Date Created
        public string Message { get; set; } //Log
        public string StackTrace { get; set; } //???
        public string Type { get; set; } //Level

        public Log()
        {
        }
    }
}
