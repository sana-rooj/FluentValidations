using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ICompanyInformation : IRepository<CompanyInformation>
    {
        bool AddPolicy(CompanyInformation companyInformation);
        bool AddHandbook(CompanyInformation companyInformation);
        bool AddOrientation(CompanyInformation companyInformation);
        long FileLength(string path);
        bool delete(int id);
        IEnumerable<CompanyInformation> GetActiveCompanyInformations();
        IEnumerable<CompanyInformation> GetPolicies();
        IEnumerable<CompanyInformation> GetHandbooks();
        IEnumerable<CompanyInformation> GetOrientations();
        FileDownload DownloadFile(int id);

    }
}