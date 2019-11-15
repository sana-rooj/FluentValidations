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
   public class CertificationSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Certifications != null && context.Certifications.Any())
                {

                } else
                {
                    var employees = GetCertifications().ToArray();
                    context.Certifications?.AddRange(employees);
                }

                context.SaveChanges();
            }
        }
        public static List<Certification> GetCertifications()
        {
          List<Certification> Certifications = new List<Certification>()
            {

                new Certification {
                    EmployeeId=1,
                    CertificateName="training", CertificationLink="ggg",
                      DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                },

                new Certification {
                    EmployeeId=1,
                    CertificateName="Development", CertificationLink="abc",CertificateScannedCopy="",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }

            };



            return Certifications;
        }
    }


}

