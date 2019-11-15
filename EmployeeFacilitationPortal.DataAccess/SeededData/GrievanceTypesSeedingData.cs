using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
    public class GrievanceTypesSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                if (context.GrievanceType != null && context.GrievanceType.Any())
                {
                    var grievanceupdatetype1 = context.GrievanceType.First(a => a.Id.Equals(1));
                    if (grievanceupdatetype1 != null)
                        grievanceupdatetype1.Type = "Bullying and Harassment";

                    var grievanceupdatetype2 = context.GrievanceType.First(a => a.Id.Equals(2));
                    if (grievanceupdatetype2 != null)
                        grievanceupdatetype2.Type = "Health and Safety";
                }
                else
                {
                    var grievances = GetGrievances().ToArray();
                    if (grievances.Any())
                        context.GrievanceType?.AddRange(grievances);
                }
                context.SaveChanges();
            }
        }
        public static List<Entities.Models.GrievanceTypes> GetGrievances()
        {
            List<Entities.Models.GrievanceTypes> grievances = new List<Entities.Models.GrievanceTypes>()
            {

                new Entities.Models.GrievanceTypes {
                 Type ="Bullying and harresment",
                 AssignedTo = 2

                },
                new Entities.Models.GrievanceTypes {
                 Type ="Health and Saftey",
                 AssignedTo = 4

                },
                new Entities.Models.GrievanceTypes {
                 Type ="Office Environment",
                 AssignedTo = 4
                },
                new Entities.Models.GrievanceTypes {
                 Type ="Team Issues",
                 AssignedTo = 2

                },
                  new Entities.Models.GrievanceTypes {
                 Type ="Organizational Changes",
                 AssignedTo = 2

                },
                    new Entities.Models.GrievanceTypes {
                 Type ="Salary Changes",
                 AssignedTo = 2

                }


            };



            return grievances;
        }
    }
}

