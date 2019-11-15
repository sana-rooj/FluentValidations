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
    public class PersonalInfoSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.PersonalInfos != null && context.PersonalInfos.Any())
                {
                   
                } else
                {
                    var personalInfos = GetPersonalInfos().ToArray();
                    context.PersonalInfos?.AddRange(personalInfos);
                }
                
                context.SaveChanges();
            }
        }

        //check emp id
        public static List<PersonalInfo> GetPersonalInfos()
        {
            List<PersonalInfo> PersonalInfoList = new List<PersonalInfo>()
            {
                new PersonalInfo
                {
                  EmployeeId=1,Email="Angular505@gmail.com",Ntn="1234567",Relative="None",Disability="None",
                  FullName ="Muhammad Ali",FatherName="Muhammad Javed",HusbandName="",Picture="",DateOfBirth=DateTime.Now,Gender="Male",
                  MaritalStatus="Married",Cnic="71179-3409123-4",PassportNumber="",PassportValidityDate=System.DateTime.Now,BloodGroup="O+",PresentAddress="",PermanentAddress="",
                  PersonalContactNumber ="",EmergencyContactNumber="",EmergencyContactPerson="",DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now
                }
            };
            return PersonalInfoList;

        }

    }
}

