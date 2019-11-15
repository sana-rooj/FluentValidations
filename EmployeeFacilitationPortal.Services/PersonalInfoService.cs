using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class PersonalInfoService : Repository<PersonalInfo>, IPersonalInfo
    {
        public DBContext _dbContext;
        public new IUnitOfWork _unitOfWork;
        private readonly IUploadernDownloader _uploadndownloadService;
        public PersonalInfoService(DBContext context, IUnitOfWork unitOfWork,IUploadernDownloader UnDService) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
            _uploadndownloadService = UnDService;
        }

        public bool UploadImage(string base64String)
        {
            throw new NotImplementedException();
        }

        public bool PersonalInfoObjectExists(int EmpId)
        {
            return _dbContext.PersonalInfos.Any(p=>p.EmployeeId.Equals(EmpId));
        }
        public PersonalInfo GetByEmail(string email)
        {
            return _dbContext.PersonalInfos.FirstOrDefault(info => info.Email.Equals(email));
        }

        public string PreviewProfileImage(string email)
        {                         
            var employee = _dbContext.Employees.FirstOrDefault(m => m.Username.Equals(email));
                if (employee != null)
                {
                return _uploadndownloadService.DownloadFileFromReference(employee.PersonalInfo.Picture,email); //Picture contains Picture/<PictureName>, email contains: Foldername
                }
                else
                {
                    return "";
                }      
          
        }
    }
}
