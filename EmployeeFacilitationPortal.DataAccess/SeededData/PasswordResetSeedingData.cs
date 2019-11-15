using EmployeeFacilitationPortal.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
   public  class PasswordResetSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.PasswordResets != null && context.PasswordResets.Any())
                {
                    var resetRequest = context.PasswordResets.FirstOrDefault(a => a.EmployeeId.Equals(1));
                    if (resetRequest != null)
                        resetRequest.UserEmail = "Angular505@gmail.com";
                } else
                {
                    var employees = GetPasswordReset().ToArray();
                    context.PasswordResets?.AddRange(employees);
                }

                
                context.SaveChanges();
            }
        }
        public static List<PasswordReset> GetPasswordReset()
        {
            List<PasswordReset> PasswordRests = new List<PasswordReset>()
            {

                new PasswordReset {
                    EmployeeId=1,
                    UserEmail="Angular505@gmail.com",
                    Password ="abc" ,
                    DateTime =System.DateTime.Now,
                    IsAlreadyRegistered =true,
                    PasswordSetStatus=false,
                    ExpiryInDays =1,
                    DateCreated =System.DateTime.Now,
                    DateModified =System.DateTime.Now
                }
            };



            return PasswordRests;
        }
    }

}
