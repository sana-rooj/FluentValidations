using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
   public class DependentSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Dependents != null && context.Dependents.Any())
                {

                } else
                {
                    var employees = GetDependents().ToArray();
                    context.Dependents?.AddRange(employees);
                }

               
                context.SaveChanges();
            }
        }
        public static List<Dependent> GetDependents()
        {
            List<Dependent> Dependents = new List<Dependent>()
            {

                new Dependent {
                   EmployeeId=1, Name="saba",Cnic="999999999999", DateOfBirth=System.DateTime.Now, Relationship="abc",
      DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                },

                new Dependent {
                   EmployeeId=1, Name="alena",Cnic="99998888", DateOfBirth=System.DateTime.Now, Relationship="xyz",
      DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                },


            };



            return Dependents;
        }
    }



}