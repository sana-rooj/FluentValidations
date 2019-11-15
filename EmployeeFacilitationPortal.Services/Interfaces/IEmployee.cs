using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;


namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IEmployee : IRepository<Employee>
    {
       
        Task<Employee> GetByUsername(string username);
        IList<Employee> GetAllEmployees(int page, string sort,bool sortOrder, string val, int pageSize,bool isActive, int roleId);
        //new IQueryable<Employee> Get(int id); 
        //Task<Employee> Get(int id);
        //Task<Task> Get(int id);
        Task<Employee> GetById(int id);
        Task<IEnumerable<Employee>> GetAll();
        //new IEnumerable<Employee> GetAll();
        //new IEnumerable<Employee> GetAll();
        IList<Employee> GetEmployeesOnValidation(int page, string sort, bool sortOrder, string val, int pageSize, bool isActive, int roleId);
        int GetCount();
        int GetInvalidatedAccountCount();
        int GetRole(string email);
        void EmpUpdate(Employee employee, bool deleteAssociatedObjects = false);
        int GetSearchedAccountsCount(string search);
        void GenerateLink(string email,int empId, string personalEmail);
        IList<Employee> GetPasswordResetList(int page, string sort, bool sortOrder, string val, int pageSize);
        int GetUnRegisteredAccountCount();
        Task<QueryResult<Employee>> GetEmployees(EmployeeQuery queryObj);
    }
}