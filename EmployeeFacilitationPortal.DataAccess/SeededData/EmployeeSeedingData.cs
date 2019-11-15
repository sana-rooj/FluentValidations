using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
    public class EmployeeSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Employees != null && context.Employees.Any())
                    return; // DB has already been seeded

                var employees = GetEmployees().ToArray();
                context.Employees?.AddRange(employees);
                context.SaveChanges();
            }
        }
        public static List<Employee> GetEmployees()
        {
            List<Employee> Employees = new List<Employee>()
            {

                new Employee {
                    RoleId =5,
                    Username ="Angular505@gmail.com",
                    DateOfJoining =System.DateTime.Now,
                    Designation ="Traniee",
                    EmpId =1,
                    IsValidated =true,
                    IsActive = true,
                    ProjectAssigned="xyz",
                    Terminated ="",
                    DateCreated =System.DateTime.Now,
                    DateModified =System.DateTime.Now },

                  
            };



            return Employees;
        }
    }
}
