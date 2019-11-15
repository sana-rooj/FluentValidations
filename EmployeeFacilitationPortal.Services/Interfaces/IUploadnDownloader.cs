using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IUploadernDownloader
    {

         string  UploadFileAsReference(string FileBase64,string EmployeeEmail,string FileName);
         string  DownloadFileFromReference(string Filename,string FilePath);


    }
}