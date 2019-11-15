using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;
using System.Linq;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IPasswordResetService : IRepository<PasswordReset>
    {
        bool check_timeout(string email);
        void generate_link(string email);

        bool PasswordReset(Login login);
        void GenerateAndSendEmail(string personalEmail, string ciklumProfileEmail);
        IList<PasswordReset> GetallUnRegistered();
        IEnumerable<PasswordReset> Getall();
        PasswordReset GetObjUsingOfficialEmail(string userEmail);
        int GetUnRegisteredCount();

    }
}
