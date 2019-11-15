using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmployeeFacilitationPortal.Entities.Common.Utility;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
    public class LoginSeedingData
    {
       
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Logins != null && context.Logins.Any())
                    return; // DB has already been seeded

                var logins= GetLogins().ToArray();
                context.Logins?.AddRange(logins);
                context.SaveChanges();
            }
        }
        public static List<Login> GetLogins()
        {
            List<Login> logins = new List<Login>()
            {
                
                  new Login {
                     EncryptedPassword = Encryptor.Encrypt("Ciklum123"),
                     Email ="Angular505@gmail.com",
                     DateCreated =System.DateTime.Now, DateModified=System.DateTime.Now
                  },
                  
            };
            return logins;
        }

        
    }

}
