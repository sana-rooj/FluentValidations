using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;


namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ICertification : IRepository<Certification>
    {
        IEnumerable<Certification> GetAll(int EmpId);
        Certification GetCertification(int id);
        bool CertificationExists(int id);

       Task<bool> Remove(int id);

        Task<bool> RemoveAllAgainst(int empId);

    }
}