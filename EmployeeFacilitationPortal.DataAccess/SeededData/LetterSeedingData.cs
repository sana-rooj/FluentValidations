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
    public class LetterSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();


                // Look for any ailments
                if (context.Letters != null && context.Letters.Any())
                {
                    var letter = context.Letters.FirstOrDefault(a => a.EmployeeId.Equals(3));
                    if (letter != null)
                    {
                        letter.EmployeeId = 1;
                        letter.Reason = "Required for medical";
                        letter.Response = "Granted";
                        letter.Category = "Medical";
                        letter.DateCreated = System.DateTime.Now;
                        letter.DateModified = System.DateTime.Now;
                    }

                    letter = context.Letters.FirstOrDefault(a => a.EmployeeId.Equals(4));
                    if (letter != null)
                    {
                        letter.EmployeeId = 1;
                        letter.Reason = "Required for application submission";
                        letter.Response = "Granted";
                        letter.Category = "Experience";
                        letter.DateCreated = System.DateTime.Now;
                        letter.DateModified = System.DateTime.Now;
                    }

                }
                else
                {
                    var letter = GetLetter().ToArray();
                    context.Letters?.AddRange(letter);
                }

               
                
                context.SaveChanges();
            }
        }


        public static List<Letter> GetLetter()
        {
            List<Letter> LetterList = new List<Letter>()
            {
                new Letter
                {
                EmployeeId=1, Reason="Required for medical", Response="Granted",Category="Medical",
                    DateCreated =System.DateTime.Now,
                    DateModified =System.DateTime.Now


                },

                new Letter

                {
              EmployeeId=1, Reason="Required for application submission", Response="Generated",Category="Experience",
                    DateCreated =System.DateTime.Now,
                    DateModified =System.DateTime.Now


                },


            };
            return LetterList;

        }
    }
}