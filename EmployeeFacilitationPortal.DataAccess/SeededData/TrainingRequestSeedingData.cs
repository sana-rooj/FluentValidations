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
   public class TrainingRequestSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.TrainingRequests != null && context.TrainingRequests.Any())
                {
                    var trainingRequest = context.TrainingRequests.FirstOrDefault(a => a.EmployeeId.Equals(2));
                    if (trainingRequest != null)
                    {
                        trainingRequest.EmployeeId = 1;
                        
                    }
                } else
                {
                    var employees = GetTrainingRequests().ToArray();
                    context.TrainingRequests?.AddRange(employees);
                }
                context.SaveChanges();
            }
        }
        public static List<TrainingRequest> GetTrainingRequests()
        {
            List<TrainingRequest> TrainingRequests = new List<TrainingRequest>()
            {

                new TrainingRequest {
                    EmployeeId=1, BussinessJustification="", DeliveryType="Physical Training",  TrainingRequestTypeId= 2, TrainingRequestTypeTitle = "Technical Skills",
                    Detail ="This is helpful", Status="Rejected", Cost=122, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                },

                new TrainingRequest {
                    EmployeeId=1, BussinessJustification="", DeliveryType="Physical Training", TrainingRequestTypeId= 1, TrainingRequestTypeTitle = "Soft Skills",
                    Detail ="This is helpful", Status="Approved", Cost=122, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }

            };

            return TrainingRequests;
        }
    }
}

