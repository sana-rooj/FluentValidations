using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string PageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


        //public ICollection<FieldPermission> FieldPermission_List { get; set; } //For to Many-to-Many relation between Page and FieldPermision

        //public ICollection<PagePermission> PagePermission_List { get; set; } // Step 1: M-to-M relation between Page and Role
    }
}
