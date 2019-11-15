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
    public class LanguageSkillsSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.LanguageSkills != null && context.LanguageSkills.Any())
                {

                }
                else
                {
                    var employeesLanguageSkills = GetLanguageSkills().ToArray();
                    context.LanguageSkills?.AddRange(employeesLanguageSkills);
                }

                
                
                context.SaveChanges();
            }
        }
        public static List<LanguageSkill> GetLanguageSkills()
        {
            List<LanguageSkill> foreignLanguagesSkill = new List<LanguageSkill>()
            {
                new LanguageSkill
                {
                    EmployeeId=1,
                    Language="English", ProficiencyLevel="Intermediate",
                    DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }

            };

            return foreignLanguagesSkill;
        }
   
    }
}
