using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeFacilitationPortal.Entities.Models
{
    public class PagePermission
    { 
        //Note: not using composite key, because that requires changes through Fluent API.
      [Key]
       public int Id { get; set; }
       
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        //public Role _Role { get; set; }

        [ForeignKey("Page")]
        public int PageId { get; set; } // I think this is needed too.
   
        //public Page _Page { get; set; }
        public bool IsAllowed { get; set; } //Confirm DataType
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    
    }
}
