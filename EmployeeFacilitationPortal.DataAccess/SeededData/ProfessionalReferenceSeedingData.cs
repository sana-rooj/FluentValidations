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
   public  class ProfessionalReferenceSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.ProfessionalReferences != null && context.ProfessionalReferences.Any()) 
                    return; // DB has already been seeded

                var professionalInfos = GetProfessionalReferences().ToArray();
                context.ProfessionalReferences?.AddRange(professionalInfos);
                context.SaveChanges();
            }
        }


        public static List<ProfessionalReference> GetProfessionalReferences()
        {
            List<ProfessionalReference> ProfessionalList = new List<ProfessionalReference>()
            {
                new ProfessionalReference
                {
                 EmployeeId=1,Name="saba", OrganizationName="abc", Designation="sw",ContactNumber="6363",
                    EmailId="saadahmad@gmail.com",  Relationship="xyz",
                    DateCreated =System.DateTime.Now,
                    DateModified =System.DateTime.Now


                }
            };
            return ProfessionalList;

        }



    }
}

