using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;

namespace EmployeeFacilitationPortal.Services
{
    public class RegisterService: Repository<Employee>, IRegister
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;
        private readonly IUploadernDownloader _uploadndownloadService;
        
        public RegisterService(DBContext context, IUnitOfWork unitOfWork,IUploadernDownloader UnDService) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
            _uploadndownloadService = UnDService;
            
        }



        public bool IsIdExist(int id) => _dbContext.Employees.Any(e => e.Id == id);
        public bool IsEmpIdExist(int EmpId) => _dbContext.Employees.Any(e => e.EmpId == EmpId);
        public void Register(Employee employee)
        {
            employee.Login.Password = RandomNumberGenerator.RandomString(8);
            _dbContext.Logins.Add(employee.Login);
            _dbContext.Add(employee);
            _dbContext.SaveChanges();
        }
        public bool PostWithImage(Employee employee)
        {
            try
            {
                var recordsByEmail = _dbContext.Employees.Where(r => r.Username == employee.Username).FirstOrDefault();
                if (recordsByEmail != null)
                {
                    return false; // because this user already exists
                }
                employee.Login.EncryptedPassword = Encryptor.Encrypt(employee.Login.Password);
                employee.PersonalInfo.Picture = _uploadndownloadService.UploadFileAsReference(employee.PersonalInfo.Picture, employee.Username, employee.PersonalInfo.FullName);             
                employee.DateCreated = System.DateTime.Now;
                employee.DateModified = System.DateTime.Now;
                employee.PersonalInfo.DateCreated = System.DateTime.Now;
                employee.PersonalInfo.DateModified = System.DateTime.Now;
                employee.DateOfJoining = System.DateTime.Now;                     
                _dbContext.Add(employee);
                return true;
            }
            catch (Exception exception)
            {
                throw exception;            
            }

        }
      
    }
}
