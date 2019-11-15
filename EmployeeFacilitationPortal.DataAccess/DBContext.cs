using System;
using EmployeeFacilitationPortal.DataRepository.SeededData;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.DataRepository
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions options) : base(options)
        {
            try
            {
                if (IsConnectionAvailable())
                    Database.Migrate();
            }
            catch (Exception ex)
            {
                // throw ex;
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

        }
        public bool IsConnectionAvailable()
        {
            bool isAvailable = false;
            try
            {
                Database.OpenConnection();
                isAvailable = true;
                Database.CloseConnection();
            }
            catch (Exception)
            {
                isAvailable = false;
            }
            finally
            {
                Database.CloseConnection();
            }
            return isAvailable;
        }
        
        public DbSet<Log> Logs { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProfessionalReference> ProfessionalReferences { get; set; }
        public DbSet<EducationalRecord> EducationalRecords { get; set; }
        public DbSet<WorkHistory> WorkHistories { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Login> Logins { get; set; }


       // public DbSet<Claim> Claims { get; set; }
        public DbSet<TechnicalSkill> TechnicalSkills { get; set; }
        public DbSet<Grievance> Grievances { get; set; }
        public DbSet<EmployeeCandidateReferred> EmployeeCandidatesReferred { get; set; }


        public DbSet<Page> Pages { get; set; }
        public DbSet<CandidateReferred> CandidatesReferred { get; set; }
        //Intermediates:  Do we need these???
        public DbSet<PagePermission> PagePermissions { get; set; }
        public DbSet<FieldPermission> FieldPermissions { get; set; }
        public DbSet<ReferredTechnicalSkill> ReferredTechnicalSkills { get; set; }


        public DbSet<Letter> Letters { get; set; }
        public DbSet<Query> Queries { get; set; }

        //================== Training request tables
        public DbSet<TrainingRequestType> TrainingRequestTypes { get; set; }
        public DbSet<TrainingRequest> TrainingRequests { get; set; }

        //========== updated field of profile 
        public DbSet<BasicInfoAttachments> AllAttachments { get; set; }

        // Required for Ciklum Profile-----
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<LanguageSkill> LanguageSkills { get; set; }
        public DbSet<EmployeeTechnicalSkills> EmployeeTechnicalSkills { get; set; }
        public DbSet<LetterRequests> LetterRequest { get; set; }
        public DbSet<LetterTypes> LetterType { get; set; }

        public DbSet<GrievanceTypes> GrievanceType { get; set; }
        public DbSet<GrievanceMessage> GrievanceMessage { get; set; }
        public DbSet<CompanyInformation> CompanyInformation { get; set; }


    }
}
