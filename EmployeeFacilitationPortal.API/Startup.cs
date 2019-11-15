using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.SeededData;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Services;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.OpenApi.Models;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.API.Filters;
using EmployeeFacilitationPortal.Entities.Validations;
using FluentValidation.AspNetCore;

namespace EmployeeFacilitationPortal.API

{
    public class Startup
    {
        private readonly string _allowSpecificOrigins = "AllowOrigin";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //connection string => appsetting.json
            Configuration.GetValue<string>("ConnectionStrings:ServerConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).
                AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFP web API", Version = "v1" });
            });
            //connection string
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")));
            services.Configure<IISOptions>(options =>  //for hosting
            {
                options.AutomaticAuthentication = false;
            });
            // ------ For encryption configurations and accessing urls ---------- 
            services.AddSingleton(Configuration.GetSection("AppConfigurations").Get<AppConfigurations>());
            // -------------------------------

            //for Authorization----
            services.AddScoped<IGenerateWebToken, AuthorizationTokenGenerationService>();

            /* ---- Authorization ------------- */
            // Replace the default authorization policy provider with our own
            // custom provider which can return authorization policies for given
            // policy names (instead of using the default policy provider)
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionsFilter>();
            // As always, handlers must be provided for the requirements of the authorization policies
            services.AddScoped<IAuthorizationHandler, PermissionsAuthorizationHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            /* -====== Authorization ends -================ */


            //    services.AddLogging();

            services.AddTransient<IQueryableExtensions<Employee>, QueryableExtensions<Employee>>();
            services.AddTransient<IQueryableExtensions<LetterRequests>, QueryableExtensions<LetterRequests>>();

            services.AddTransient<IQueryableExtensions<TrainingRequest>, QueryableExtensions<TrainingRequest>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegister, RegisterService>();
            services.AddScoped<IRole, RoleService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IPersonalInfo, PersonalInfoService>();
            services.AddScoped<ICertification, CertificationService>();
            services.AddScoped<IUploadernDownloader, UploadnDownloadService>();
            services.AddScoped<ILetterRequests, LetterRequestService>();
            services.AddScoped<ILetterTypes, LetterTypeService>();
            services.AddScoped<IGrievanceTypes, GrievanceTypeService>();
            services.AddScoped<IGrievance, GrievanceService>();
            services.AddScoped<ICompanyInformation, CompanyInformationService>();
            services.AddScoped<ITrainingRequestService, TrainingRequestService>();
            services.AddScoped<IPagePermission, PagePermissionService>();
           
            services.AddCors(c =>
            {
                c.AddPolicy(_allowSpecificOrigins, options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });


           
            

            // for fluent validations 
            services.AddTransient<FluentValidation.IValidator<Employee>, EmployeeValidator>();
        }

      
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //================= Authorization ============
            app.UseAuthentication();
            //====================================


            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFP Web API");
            });
            
           
            app.UseCors(_allowSpecificOrigins);
        

            RoleSeedingData.Initialize(app);
            EmployeeSeedingData.Initialize(app);
            PageSeedingData.Initialize(app);
            PersonalInfoSeedingData.Initialize(app);
            ProfessionalReferenceSeedingData.Initialize(app);
            LetterSeedingData.Initialize(app);
            WorkHistoriesSeedingData.Initialize(app);
            LoginSeedingData.Initialize(app);
            PagePermissionSeedingData.Initialize(app);
            FeildPermissionSeedingData.Initialize(app);
            EducationalRecordSeedingData.Initialize(app);
            DependentSeedingData.Initialize(app);
            CertificationSeedingData.Initialize(app);
            LetterTypeSeedingData.Initialize(app);
            PasswordResetSeedingData.Initialize(app);
            LetterRequestSeedingData.Initialize(app);
            TrainingRequestSeedingData.Initialize(app);
            TrainingRequestTypeSeedingData.Initialize(app);
            GrievanceTypesSeedingData.Initialize(app);
         
        }
    }
}
