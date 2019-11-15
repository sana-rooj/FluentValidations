using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class LoginCredentialsEmail
    {
            [Key]
            public int Id { get; set; }

            public string RecipientEmail { get; set; }

            public string EmailBody { get; set; }

            public DateTime DateTime { get; set; }

    }
}
