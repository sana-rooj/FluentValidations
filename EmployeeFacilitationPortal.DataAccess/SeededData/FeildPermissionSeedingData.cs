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
    public class FeildPermissionSeedingData
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.FieldPermissions != null && context.FieldPermissions.Any())
                    return; // DB has already been seeded

                var feildPermission = GetFeildData().ToArray();
                context.FieldPermissions?.AddRange(feildPermission);
                context.SaveChanges();
            }
        }


        public static List<FieldPermission> GetFeildData()
        {
            List<FieldPermission> feilds = new List<FieldPermission>()
            {
                //for employee
                new FieldPermission {RoleId=1,PageId=4,FieldName="RoleId",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                  new FieldPermission {RoleId=1,PageId=4,FieldName="EmpId",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                  new FieldPermission {RoleId=1,PageId=4,FieldName="Username",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                    new FieldPermission{RoleId=1,PageId=4,FieldName="DateOfJoining",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                       new FieldPermission{RoleId=1,PageId=4,FieldName="Designation",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                          new FieldPermission{RoleId=1,PageId=4,FieldName="ProjectAssigned",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                             new FieldPermission{RoleId=1,PageId=4,FieldName="terminated",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                new FieldPermission{RoleId=1,PageId=4,FieldName="DateCreated",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                   new FieldPermission{RoleId=1,PageId=4,FieldName="DateModified",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                      new FieldPermission{RoleId=1,PageId=4,FieldName="Email",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                         new FieldPermission{RoleId=1,PageId=4,FieldName="Ntn",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                            new FieldPermission{RoleId=1,PageId=4,FieldName="Disability",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                               new FieldPermission{RoleId=1,PageId=4,FieldName="FullName",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                  new FieldPermission{RoleId=1,PageId=4,FieldName="HusbandName",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                     new FieldPermission{RoleId=1,PageId=4,FieldName="Picture",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                        new FieldPermission{RoleId=1,PageId=4,FieldName="DateOfBirth",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                           new FieldPermission{RoleId=1,PageId=4,FieldName="Gender",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                              new FieldPermission{RoleId=1,PageId=4,FieldName="MaritalStatus",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                 new FieldPermission{RoleId=1,PageId=4,FieldName="Cnic",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                    new FieldPermission{RoleId=1,PageId=4,FieldName="PassportNumber",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                       new FieldPermission{RoleId=1,PageId=4,FieldName="PassportValidityDate",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                          new FieldPermission{RoleId=1,PageId=4,FieldName="BloodGroup",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                             new FieldPermission{RoleId=1,PageId=4,FieldName="PresentAddress",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                new FieldPermission{RoleId=1,PageId=4,FieldName="PermanentAddress",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                   new FieldPermission{RoleId=1,PageId=4,FieldName="PersonalContactNumber",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                      new FieldPermission{RoleId=1,PageId=4,FieldName="EmergencyContactNumber",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                         new FieldPermission{RoleId=1,PageId=4,FieldName="EmergencContactPerson",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

               
                    //for HR
                  new FieldPermission {RoleId=2,PageId=4,FieldName="RoleId",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                  new FieldPermission {RoleId=2,PageId=4,FieldName="EmpId",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                  new FieldPermission {RoleId=2,PageId=4,FieldName="Username",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                    new FieldPermission{RoleId=2,PageId=4,FieldName="DateOfJoining",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                       new FieldPermission{RoleId=2,PageId=4,FieldName="Designation",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                          new FieldPermission{RoleId=2,PageId=4,FieldName="ProjectAssigned",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                             new FieldPermission{RoleId=2,PageId=4,FieldName="terminated",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                new FieldPermission{RoleId=2,PageId=4,FieldName="DateCreated",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                   new FieldPermission{RoleId=2,PageId=4,FieldName="DateModified",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                      new FieldPermission{RoleId=2,PageId=4,FieldName="Email",IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                         new FieldPermission{RoleId=2,PageId=4,FieldName="Ntn",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                            new FieldPermission{RoleId=2,PageId=4,FieldName="Disability",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                               new FieldPermission{RoleId=2,PageId=4,FieldName="FullName",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                  new FieldPermission{RoleId=2,PageId=4,FieldName="HusbandName",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                     new FieldPermission{RoleId=2,PageId=4,FieldName="Picture",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                        new FieldPermission{RoleId=2,PageId=4,FieldName="DateOfBirth",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                           new FieldPermission{RoleId=2,PageId=4,FieldName="Gender",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                              new FieldPermission{RoleId=2,PageId=4,FieldName="MaritalStatus",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                 new FieldPermission{RoleId=2,PageId=4,FieldName="Cnic",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                    new FieldPermission{RoleId=2,PageId=4,FieldName="PassportNumber",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                       new FieldPermission{RoleId=2,PageId=4,FieldName="PassportValidityDate",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                          new FieldPermission{RoleId=2,PageId=4,FieldName="BloodGroup",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                             new FieldPermission{RoleId=2,PageId=4,FieldName="PresentAddress",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                new FieldPermission{RoleId=2,PageId=4,FieldName="PermanentAddress",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                   new FieldPermission{RoleId=2,PageId=4,FieldName="PersonalContactNumber",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                      new FieldPermission{RoleId=2,PageId=4,FieldName="EmergencyContactNumber",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                                                                                         new FieldPermission{RoleId=2,PageId=4,FieldName="EmergencContactPerson",IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now}


            };
            return feilds;
        }
    }
}
