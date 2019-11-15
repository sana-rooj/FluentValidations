using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;

namespace EmployeeFacilitationPortal.Services
{
    public class TrainingRequestService : Repository<TrainingRequest>, ITrainingRequestService
    {
        public DBContext _dbContext;
        public new IUnitOfWork _unitOfWork;
        private readonly IQueryableExtensions<TrainingRequest> _queryableExtensions;
        public TrainingRequestService(DBContext context, IUnitOfWork unitOfWork, IQueryableExtensions<TrainingRequest> query) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;

            _queryableExtensions = query;
        }

        public IList<TrainingRequestType> GetTrainingRequestTypes()
        {
            return new List<TrainingRequestType>(_dbContext.TrainingRequestTypes);
        }

        public async Task<TrainingRequest> GetAllTrainingRequestsWithId(int requestId)
        {
            // before returning this record, get employee name from employees table and update this record with employee name
            var newTrainingRequest = await (_dbContext.TrainingRequests.Where(r => r.Id == requestId)).FirstOrDefaultAsync<TrainingRequest>();
            newTrainingRequest.EmployeeName = await _dbContext.Employees
                .Where(e => e.Id == newTrainingRequest.EmployeeId)
                .Select(e => e.PersonalInfo.FullName)
                .FirstOrDefaultAsync();
            return newTrainingRequest;
        }

        public async  Task<bool> PostTrainingRequest(TrainingRequest newTrainingRequest)
        {
            try
            {

                newTrainingRequest.DateCreated = LocalTimeService.ToLocalTime();
                newTrainingRequest.DateModified = LocalTimeService.ToLocalTime();

                if (newTrainingRequest.EmployeeId > 0)
                {
                    _dbContext.TrainingRequests.Add(newTrainingRequest);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateRecord(TrainingRequest trainingRequest) // used to update record usin request Id
        {
            try
            {
                trainingRequest.DateModified = System.DateTime.Now;

                _dbContext.TrainingRequests.Update(trainingRequest);
                await _unitOfWork.Complete();
                return true;
            }
            catch
            {
                if (!TrainingRequestExists(trainingRequest.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public IList<TrainingRequest> GetPaginatedEmployeeTrainingRequests(int empId, int page = 1, string sort = "Id", string searchedTerm = "", int limit = -1, bool sortOrder = false, string searchInColumn = "none", string thenSearchFor ="")
        {
            IQueryable<TrainingRequest> trainingRequests;
            if (empId > 0)
            {
                trainingRequests = _dbContext.TrainingRequests.Where(L => L.EmployeeId == empId);
            }
            else
            {
                trainingRequests = _dbContext.TrainingRequests;
            }

            if (trainingRequests.Count()<1){
                return null;
            }

            // send record count in returned list
            int totalPageCountAccordingToCurrentLimit = 1;
            if (limit>0)
            {
                totalPageCountAccordingToCurrentLimit = (trainingRequests.Count() + limit - 1) / limit;
            }

            trainingRequests = trainingRequests.Select(a => new TrainingRequest()
            {
                Cost = a.Cost,
                DateCreated = a.DateCreated,
                DateModified = a.DateModified,
                DeliveryType = a.DeliveryType,
                EmployeeId = a.EmployeeId,
                EmployeeName = _dbContext.Employees.Where(e => e.Id == a.EmployeeId).FirstOrDefault().PersonalInfo.FullName,
                TotalRecordsCountAccordingToCurrentPageLimit = totalPageCountAccordingToCurrentLimit,
                Id = a.Id,
                TrainingRequestTypeId = a.TrainingRequestTypeId,
                TrainingRequestTypeTitle = a.TrainingRequestTypeTitle,
                BussinessJustification = a.BussinessJustification,
                Detail = a.Detail,
                Status = a.Status
            });

            if (searchedTerm != "" && searchedTerm != null && searchedTerm != "nothing")
            {
                if (searchInColumn != "none")
                {
                    trainingRequests = trainingRequests.Where(R => R.GetType().GetProperty(searchInColumn).GetValue(R,null).Equals(searchedTerm));
                    if (thenSearchFor != "" && thenSearchFor != null)
                    {
                        thenSearchFor = thenSearchFor.ToLower();
                        // go for wild search
                        trainingRequests = trainingRequests.Where(
                        L => (L.TrainingRequestTypeTitle.ToLower().Contains(thenSearchFor) ||
                        L.Status.ToLower().Contains(thenSearchFor) ||
                        //L.BussinessJustification.ToLower().Contains(thenSearchFor) ||
                        //L.Cost.ToString().Contains(thenSearchFor) ||
                        //L.EmployeeId.ToString().Contains(thenSearchFor) ||
                        L.EmployeeName.ToLower().Contains(thenSearchFor) ||
                        //L.Detail.ToLower().Contains(thenSearchFor) ||
                        L.DeliveryType.ToLower().Contains(thenSearchFor) ||
                        //L.DateCreated.ToString().Contains(thenSearchFor)) ||
                        L.DateModified.ToString().Contains(thenSearchFor)));
                    }

                }

                else
                {
                    searchedTerm = searchedTerm.ToLower();
                    // go for wild search
                    trainingRequests = trainingRequests.Where(
                    L => (L.TrainingRequestTypeTitle.ToLower().Contains(searchedTerm) ||
                    L.Status.ToLower().Contains(searchedTerm) ||
                    //L.BussinessJustification.ToLower().Contains(searchedTerm) ||
                    //L.Cost.ToString().Contains(searchedTerm) ||
                    //L.EmployeeId.ToString().Contains(searchedTerm) ||
                    L.EmployeeName.ToLower().Contains(searchedTerm) ||
                    L.Detail.ToLower().Contains(searchedTerm) ||
                    L.DeliveryType.ToLower().Contains(searchedTerm) ||
                    //L.DateCreated.ToString().Contains(searchedTerm) ||
                    L.DateModified.ToString().Contains(searchedTerm)));
                }
                
                // pagination should be updated as well because records have been removed
                if (limit > 0)
                {
                    totalPageCountAccordingToCurrentLimit = (trainingRequests.Count() + limit - 1) / limit;
                }

            }

            if (sortOrder == true)
            {
                //trainingRequests = trainingRequests.OrderBy(L => EF.Property<object>(L, sort));
                trainingRequests = trainingRequests.OrderBy(i => i.GetType().GetProperty(sort).GetValue(i, null));
            }
            else
            {
               // trainingRequests = trainingRequests.OrderBy(L => EF.Property<object>(L, sort));
                trainingRequests = trainingRequests.OrderByDescending(i => i.GetType().GetProperty(sort).GetValue(i, null));
            }

            if (limit>=0)
            {
                trainingRequests = trainingRequests.Skip((page - 1) * limit).Take(limit);
            } else
            {
                trainingRequests = trainingRequests.Skip((page - 1));
            }

            IList<TrainingRequest> trainingRequestsList = trainingRequests.ToList();
            if (trainingRequests.Count()>0)
            {
                trainingRequestsList[0].TotalRecordsCountAccordingToCurrentPageLimit = totalPageCountAccordingToCurrentLimit;
            }
            return trainingRequestsList;


        }
        private bool TrainingRequestExists(int id)
        {
            return _dbContext.TrainingRequests.Any(e => e.Id == id);
        }
        private IQueryable<TrainingRequest> ApplyFiltering(IQueryable<TrainingRequest> query, TrainingRequestsQuery queryObj)
        {
            // Wild search
            if (!string.IsNullOrWhiteSpace(queryObj.Search))
                query = query.Where(a => a.DateCreated.ToString().Contains(queryObj.Search)
                                         || a.Status.Contains(queryObj.Search)
                                         || a.TrainingRequestTypeTitle.Contains(queryObj.Search)
                                         || a.EmployeeName.Contains(queryObj.Search)

                );

            if (!string.IsNullOrWhiteSpace(queryObj.Status))
                query = query.Where(v => v.Status.Contains(queryObj.Status));
            if (!string.IsNullOrWhiteSpace(queryObj.EmployeeName))
                query = query.Where(v => v.Status.Contains(queryObj.Status));

            if (!string.IsNullOrWhiteSpace(queryObj.Type))
                query = query.Where(v => v.TrainingRequestTypeTitle.Contains(queryObj.Type));


            return query;
        }
        private IQueryable<TrainingRequest> GetQueryableAllTrainingRequests()
        {
            return _dbContext.TrainingRequests;
        }
        private IQueryable<TrainingRequest> GetQueryableUsersTrainingRequests(int id)
        {
            return _dbContext.TrainingRequests.Where(v=>v.EmployeeId.Equals(id));
        }


        public async Task<QueryResult<TrainingRequest>> GetTrainingRequests(TrainingRequestsQuery queryObj)
        {
            var result = new QueryResult<TrainingRequest>();

            IQueryable<TrainingRequest> query = null;
            if (queryObj.ViewAll.HasValue && queryObj.EmployeeId.HasValue==false)
            {
                query = GetQueryableAllTrainingRequests();
            }
            if (queryObj.ViewAll.HasValue==false && queryObj.EmployeeId.HasValue)
            {
                if (queryObj.EmployeeId != null) query = GetQueryableUsersTrainingRequests(queryObj.EmployeeId.Value);
            }
            if (queryObj.ViewAll.HasValue==false && queryObj.EmployeeId.HasValue == false)
            {
                query = GetQueryableAllTrainingRequests();
            }

            query = ApplyFiltering(query, queryObj);
            var columnsMap = new Dictionary<string, Expression<Func<TrainingRequest, object>>>()
            {
                ["status"] = trainingRequest => trainingRequest.Status,
                ["type"] = trainingRequest => trainingRequest.TrainingRequestTypeTitle,
                ["date"] = trainingRequest => trainingRequest.DateCreated,
                ["employeeName"] = trainingRequest => trainingRequest.EmployeeName,


            };
            query = _queryableExtensions.ApplyOrderingColumnMap(query, queryObj, columnsMap);
            result.TotalItems = await query.CountAsync();
            query = _queryableExtensions.ApplyPaging(query, queryObj);
            result.Items = await query.ToListAsync();
            return result;
        }

    }
}
