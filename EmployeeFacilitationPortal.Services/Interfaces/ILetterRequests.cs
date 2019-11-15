using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.DataRepository.Repository;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ILetterRequests : IRepository<LetterRequests>
    {
        // void PostLetterRequest(Letter letter); //This still needs to be written.
        Task<bool> PostLetterRequest(LetterRequests letter);
        IList<LetterRequests> getAllUserLetters(int userId);
        IList<LetterTypes> getAllLetterTemplates();
        IList<LetterRequests> getAllLetterRequests();
        IList<LetterRequests> GetPaginatedEmployeeLetterRequests(int userId,int page=1, string sort="Id", string search="", int limit=10, string SortOrder = "false");
        int GetTotalCount();
        int GetEmployeeLetterRequestsCount(int userId);
        LetterRequests getSingleLetterRequest(int id);
        IEnumerable<LetterRequests> GetAllLetterRequests(int page, string sort, string search, int limit, string SortOrder);
        Task<QueryResult<LetterRequests>> GetLetterRequests(LetterRequestsQuery queryObj);
    }
}
