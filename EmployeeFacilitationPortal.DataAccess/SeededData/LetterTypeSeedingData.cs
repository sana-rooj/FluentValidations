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
    public class LetterTypeSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.LetterType != null && context.LetterType.Any())

                    return; // DB has already been seeded


                var letter = GetLetter().ToArray();
                context.LetterType?.AddRange(letter);
                context.SaveChanges();
            }
        }


        public static List<LetterTypes> GetLetter()
        {
            List<LetterTypes> LetterTypeList = new List<LetterTypes>()
            {
                new LetterTypes
                {
                
                    Type = "EVL Bank ",
                    Description = " Data needed from your side: /n Month /n Year  /n Reason"
                },
                new LetterTypes
                {
                     Type = "EVL Travel",
                    Description = "Data needed from your side: /n Month /n Duration /n Distance"

                },
                new LetterTypes
                {
                    Type = "EVL Experience",
                    Description= "Data needed from your side: /n Reason"
                    
                },
                new LetterTypes
                {
                    Type = "Others",
                    Description = "Data needed from your side: /n Reason /n Address /n Details  "
                }


            };
            return LetterTypeList;

        }
    }
}

