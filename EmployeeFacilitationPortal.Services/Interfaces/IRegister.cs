using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IRegister:IRepository<Employee>
    {
        void Register(Employee employee);
        bool PostWithImage(Employee employee);
       
        bool IsEmpIdExist(int EmpId);
    }
}
