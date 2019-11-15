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
    public class WorkHistoriesSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.WorkHistories != null && context.WorkHistories.Any())
                    return; // DB has already been seeded

                var employees = GetWorkHistories().ToArray();
                context.WorkHistories?.AddRange(employees);
                context.SaveChanges();
            }
        }
        public static List<WorkHistory> GetWorkHistories()
        {
            List<WorkHistory> Histories = new List<WorkHistory>()
            {

                new WorkHistory {
                   EmployeeId=1, OrganizationName="Ciklum",StartDate=System.DateTime.Now,EndDate=System.DateTime.Now,
                   ProjectDescriptionAndResponsibilities="abcd",
                   ReasonForLeaving=" ", LastDrawnSalary=9000, Benefit="", Designation="IT",  DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }
            };



            return Histories;

        }

    }
}