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
    public class PagePermissionSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.PagePermissions != null && context.PagePermissions.Any())
                    return; // DB has already been seeded

                var pagePermission = GetPagesData().ToArray();
                context.PagePermissions?.AddRange(pagePermission);
                context.SaveChanges();
            }
        }

        public static List<PagePermission> GetPagesData()
        {
            List<PagePermission> pages = new List<PagePermission>()
            {
                //for employee
                // 1- Login
                new PagePermission {RoleId=1,PageId=1,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=1,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=1,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=1,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=1,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 2- Company-Information/Handbook 
                new PagePermission {RoleId=1,PageId=2,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=2,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=2,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=2,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=2,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 3- Company-Information/Policy
                new PagePermission {RoleId=1,PageId=3,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=3,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=3,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=3,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=3,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 4- Company-Information
                new PagePermission {RoleId=1,PageId=4,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=4,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=4,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=4,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=4,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},


                // 5- Training/List
                new PagePermission {RoleId=1,PageId=5,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=5,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=5,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=5,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=5,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 6- Letter/List
                new PagePermission {RoleId=1,PageId=6,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=6,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=6,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=6,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=6,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 7- Requests
                new PagePermission {RoleId=1,PageId=7,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=7,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=7,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=7,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=7,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 8- Grievance/List
                new PagePermission {RoleId=1,PageId=8,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=8,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=8,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=8,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=8,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 9- Grievance/Create
                new PagePermission {RoleId=1,PageId=9,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=9,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=9,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=9,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=9,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 10- Training/Create
                new PagePermission {RoleId=1,PageId=10,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=10,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=10,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=10,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=10,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 11- Letter/Create
                new PagePermission {RoleId=1,PageId=11,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=11,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=11,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=11,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=11,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 12- Request
                new PagePermission {RoleId=1,PageId=12,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=12,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=12,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=12,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=12,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 13- Request/Training/View/:id
                new PagePermission {RoleId=1,PageId=13,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=13,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=13,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=13,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=13,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 14- Request/Letter/View/:id
                new PagePermission {RoleId=1,PageId=14,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=14,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=14,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=14,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=14,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},

                // 15- Request/Letter/Reply/:id
                new PagePermission {RoleId=2,PageId=15,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=15,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 16- Logs
                new PagePermission {RoleId=2,PageId=16,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=16,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
               

                // 17- Grievance-Forum/View/:id
                new PagePermission {RoleId=2,PageId=17,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=17,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 18- Grievance-Forum/View
                new PagePermission {RoleId=1,PageId=18,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=18,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=18,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=18,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=18,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 19- Ciklum-Profile
                new PagePermission {RoleId=1,PageId=19,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=19,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=19,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=19,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=19,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 20- Ciklum-Profile/:id
                new PagePermission {RoleId=2,PageId=20,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=20,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 21- Ciklum-Profile/Download
                new PagePermission {RoleId=1,PageId=21,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=21,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=21,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=21,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=21,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 22- Employee/List
                new PagePermission {RoleId=2,PageId=22,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=22,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 23- Profile/:id
                new PagePermission {RoleId=2,PageId=23,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=23,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 24- Profile
                new PagePermission {RoleId=1,PageId=24,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=24,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=24,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=24,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=24,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 25- Dashboard
                new PagePermission {RoleId=1,PageId=25,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=25,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=25,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=25,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=25,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 26- Register
                new PagePermission {RoleId=2,PageId=26,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=26,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 27- Password/Update/:email
                new PagePermission {RoleId=1,PageId=27,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=27,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=27,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=27,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=27,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                // 28- Error
                new PagePermission {RoleId=1,PageId=28,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=28,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=28,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=28,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=28,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
               
                
                // 29- Company-Information/Orientation
                new PagePermission {RoleId=1,PageId=29,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=29,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=29,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=29,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=29,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                
                // 30- Unauthorized-Access
                new PagePermission {RoleId=1,PageId=30,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=2,PageId=30,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=3,PageId=30,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=4,PageId=30,IsAllowed=true, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new PagePermission {RoleId=5,PageId=30,IsAllowed=false, DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
            };
            return pages;
        }



    }
}
