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
    public class LetterRequestSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.LetterRequest != null && context.LetterRequest.Any())
                    return; // DB has already been seeded

                var samples = GetLetterRequests().ToArray();
                context.LetterRequest?.AddRange(samples);
                context.SaveChanges();
            }
        }
        public static List<LetterRequests> GetLetterRequests()
        {
            List<LetterRequests> letterRequests = new List<LetterRequests>() {
              new LetterRequests {EmployeeName="Saad", LetterTypeId = 1,UserId=1, Information="Hi There",Status="Pending",Urgency="Normal",DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now}
              //new LetterRequests { EmployeeName="Fahad",LetterTypeId= 2, UserId=2, Information="No Way", Status="Rejected", Urgency="Critical",DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
              // new LetterRequests { EmployeeName="Rida",LetterTypeId= 2, UserId=2, Information="Help Needed", Status="Ready To Collect", Urgency="Critical",DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
              //  new LetterRequests {EmployeeName="Sana",LetterTypeId= 3, UserId=1, Information="Questionable Judgment", Status="Rejected", Urgency="Normal",DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                };
            return letterRequests;
        }


    }
}
