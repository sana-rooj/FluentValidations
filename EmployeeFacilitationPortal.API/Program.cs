using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeFacilitationPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateWebHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    // DataContext is basically your dbcontext file
            //    var context = services.GetRequiredService<DBContext>();
            //    context.Database.Migrate();
            //}
            //host.Run();
            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
            
    }
}
