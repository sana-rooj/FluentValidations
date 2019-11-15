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
   public class EducationalRecordSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.EducationalRecords != null && context.EducationalRecords.Any())
                {

                }
                else
                {
                    var educationrecord = GetEducationRecord().ToArray();
                    context.EducationalRecords?.AddRange(educationrecord);
                }
                                
                context.SaveChanges();
            }
        }
        public static List<EducationalRecord> GetEducationRecord()
        {

            List<EducationalRecord> EducationRecord = new List<EducationalRecord>()
            {
              new EducationalRecord
                {

                    EmployeeId=1,StartDate=System.DateTime.Now, EndDate=System.DateTime.Now, InstitutionName="abcd",Grade=3,Study="",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now, Degree="sw"
                },


              new EducationalRecord
                {

                    EmployeeId=1,StartDate=System.DateTime.Now, EndDate=System.DateTime.Now, InstitutionName="abcd",Grade=6,Study="",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now, Degree="sw"
                }

            };
            return EducationRecord;


        }
    }
}
