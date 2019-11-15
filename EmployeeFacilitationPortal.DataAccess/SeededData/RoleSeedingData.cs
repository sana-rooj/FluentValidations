using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
  public  class RoleSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Roles != null && context.Roles.Any())
                    return; // DB has already been seeded

                var roles = GetRoles().ToArray();
                context.Roles?.AddRange(roles);
                context.SaveChanges();
            }
        }
        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>()
            {
                new Role {Name = "Employee", DateCreated = System.DateTime.Now, DateModified = System.DateTime.Now },
                  new Role {Name = "HR", DateCreated = System.DateTime.Now, DateModified = System.DateTime.Now },
                  new Role { Name = "Finance", DateCreated = System.DateTime.Now, DateModified = System.DateTime.Now },
                  new Role {  Name = "Admin", DateCreated = System.DateTime.Now, DateModified = System.DateTime.Now },
                  new Role {  Name = "Super Admin", DateCreated = System.DateTime.Now, DateModified = System.DateTime.Now }

            };
            return roles;
        }
    }
}
