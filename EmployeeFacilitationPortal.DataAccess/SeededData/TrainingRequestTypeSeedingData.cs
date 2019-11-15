using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.DataRepository.SeededData
{
    public class TrainingRequestTypeSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.TrainingRequestTypes != null && context.TrainingRequestTypes.Any())
                    return; // DB has already been seeded

                var trainingRequestTypes = GetTrainingRequestTypes().ToArray();
                context.TrainingRequestTypes?.AddRange(trainingRequestTypes);
                context.SaveChanges();
            }
        }
        public static List<TrainingRequestType> GetTrainingRequestTypes()
        {
            List<TrainingRequestType> TraingRequestTypes = new List<TrainingRequestType>()
            {
                new TrainingRequestType {
                    Type = "Soft Skills",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                },

                new TrainingRequestType {
                    Type = "Technical Skills",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }

            };
            return TraingRequestTypes;
        }
    }
}
