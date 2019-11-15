using EmployeeFacilitationPortal.Entities.Models;
using System;
using EmployeeFacilitationPortal.DataRepository.Repository;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ITrainingRequestService : IRepository<TrainingRequest>
    {
        Task<bool> PostTrainingRequest(TrainingRequest newTrainingRequest);
        Task<bool> UpdateRecord(TrainingRequest trainingRequest); // used to update record usin request Id

        IList<TrainingRequestType> GetTrainingRequestTypes();

        Task<TrainingRequest> GetAllTrainingRequestsWithId(int requestId);

        IList<TrainingRequest> GetPaginatedEmployeeTrainingRequests(int empId, int page = 1, string sort = "Id", string search = "", int limit = 10, bool sortOrdered= false, string searchInColumn = "none" , string thenSearchFor = "");
        Task<QueryResult<TrainingRequest>> GetTrainingRequests(TrainingRequestsQuery queryObj);
    }
}
