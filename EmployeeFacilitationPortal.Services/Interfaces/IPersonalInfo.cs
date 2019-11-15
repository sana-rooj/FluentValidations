using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IPersonalInfo:IRepository<PersonalInfo>
    {
        PersonalInfo GetByEmail(string email);
        //var picture(int id);
        bool UploadImage(string base64String);

        bool PersonalInfoObjectExists(int EmpId);
        string PreviewProfileImage(string email);
    }
}
