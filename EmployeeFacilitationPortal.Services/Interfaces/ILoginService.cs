using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ILoginService: IRepository<Login>
    {
        bool VerifyEmail(string Email, string pass);
        
        Employee GetEmployeeByEmail(string Email);
    }
}
