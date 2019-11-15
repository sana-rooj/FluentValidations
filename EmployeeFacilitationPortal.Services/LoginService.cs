using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public class LoginService : Repository<Login>, ILoginService
    {

        private DBContext _dbContext;
        private new IUnitOfWork _unitOfWork;
        private Login LoginObject;
        public LoginService(DBContext context, IUnitOfWork unitOfWork) :base(context,unitOfWork)
        { 
            _dbContext = context;
           
            _unitOfWork = unitOfWork;
        }

        public bool VerifyEmail(string Email, string pass)
        {
            if (RegisteredUserEmailExists(Email))
            {
                foreach (var v in _dbContext.Logins)
                {
                    if (v.Email.Equals(Email))
                    {
                        LoginObject = v;
                        var decryptedPassword = Encryptor.Decrypt(LoginObject.EncryptedPassword);
                        
                        if (decryptedPassword.Equals(pass))
                        {
                            return true;
                        }
                        
                        return false;
                    }

                }
            }
            return false;
        }

        private bool RegisteredUserEmailExists(string email)
        {
            return (_dbContext.Logins.Any(e => e.Email == email));
        }

        public Employee GetEmployeeByEmail(string Email)
        {
            var EmpPI =  _dbContext.PersonalInfos.Where(p => string.Equals(p.Email,Email));
            PersonalInfo X = EmpPI.Cast<PersonalInfo>().ToList()[0];
          
            var Emp = _dbContext.Employees.Where(p => (p.Id.Equals(X.EmployeeId)));
            Employee E = Emp.Cast<Employee>().ToList()[0];
            return E;
           
        }

    }
}
