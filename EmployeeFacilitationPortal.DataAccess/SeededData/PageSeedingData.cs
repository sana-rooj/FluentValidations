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
    public class PageSeedingData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.Migrate();

                // Look for any ailments
                if (context.Pages != null && context.Pages.Any())
                    return; // DB has already been seeded

                var page = GetPages().ToArray();
                context.Pages?.AddRange(page);
                context.SaveChanges();
            }
        }
        public static List<Page> GetPages()
        {
            List<Page> pages = new List<Page>()
            {
                new Page {Name= "LoginComponent",PageUrl="Login", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "ErrorHandlerComponent",PageUrl="Error", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "UpdatePasswordComponent",PageUrl="Password/Update/:email", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "RegisterComponent",PageUrl="Register", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "DashboardComponent",PageUrl="Dashboard", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "ProfileComponent",PageUrl="Profile", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "ProfileComponent",PageUrl="Profile/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "EmployeeListComponent",PageUrl="Employee/List", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "CiklumProfilePdfComponent",PageUrl="Ciklum-Profile/Download", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "CiklumProfileComponent",PageUrl="Ciklum-Profile/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "CiklumProfileComponent",PageUrl="Ciklum-Profile", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "GrievanceForumComponent",PageUrl="Grievance-Forum/View", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "GrievanceForumComponent",PageUrl="Grievance-Forum/View/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "LogComponent",PageUrl="Logs", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "LetterRequestReplyComponent",PageUrl="Request/Letter/Reply/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "LetterRequestDetailsComponent",PageUrl="Request/Letter/View/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "TrainingRequestDetailsComponent",PageUrl="Request/Training/View/:id", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "RequestComponent",PageUrl="Request", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "LetterRequestCreateComponent",PageUrl="Letter/Create", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "TrainingRequestCreateComponent",PageUrl="Training/Create", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "GrievanceCreateComponent",PageUrl="Grievance/Create", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "GrievancesListComponent",PageUrl="Grievance/List", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "RequestsListComponent",PageUrl="Requests", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "LetterRequestListComponent",PageUrl="Letter/List", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "TrainingRequestListComponent",PageUrl="Training/List", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "CompanyInformationComponent",PageUrl="Company-Information", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "PolicyComponent",PageUrl="Policy", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "HandbookComponent",PageUrl="Handbook", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "OrientationComponent",PageUrl="Orientation", DateCreated=System.DateTime.Now,DateModified=System.DateTime.Now},
                new Page {Name= "Unauthorized-Access", PageUrl="Unauthorized-Access", DateCreated=System.DateTime.Now, DateModified= System.DateTime.Now}
            };
            return pages;
      }

     }
}
