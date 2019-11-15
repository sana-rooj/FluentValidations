using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

       // public ICollection<Employee> Employees { get; set; }
        public ICollection<FieldPermission> FieldPermission_List { get; set; } //For to Many-to-Many relation between Page and FieldPermision
        public ICollection<PagePermission> PagePermission_List { get; set; } // Step 1: M-to-M relation between Page and Role

    }
}
