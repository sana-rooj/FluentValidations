using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class FieldPermission
    {
        //Note: not using composite key, because that requires changes through Fluent API.
          [Key]
          public int Id { get; set; }
 
        [ForeignKey("Role")]

        public int RoleId { get; set; }
     
        [ForeignKey("Page")]
        public int PageId { get; set; }
        [MaxLength(50)]
        public string FieldName { get; set; }
        public bool IsAllowed { get; set; } //Confirm DataType

        //public Page _Page { get; set; }
        //public Role _Role { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


    }
}
