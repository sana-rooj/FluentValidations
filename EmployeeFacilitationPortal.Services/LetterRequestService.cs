using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class LetterRequestService : Repository<LetterRequests>, ILetterRequests
    {
        public DBContext _dbContext;
        public new IUnitOfWork _unitOfWork;
        private readonly IQueryableExtensions<LetterRequests> _queryableExtensions;
        public LetterRequestService(DBContext context, IUnitOfWork unitOfWork, IQueryableExtensions<LetterRequests> query) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
            _queryableExtensions = query;
        }

        private LetterRequests AttachLetterType(LetterRequests letterRequest)
        {
            letterRequest.LetterType = _dbContext.LetterType.FirstOrDefault(T => T.Id.Equals(letterRequest.LetterTypeId));
            return letterRequest;
        }
        public IList<LetterRequests> getAllLetterRequests()
        {
            throw new NotImplementedException();
        }

        public IList<LetterTypes> getAllLetterTemplates()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            return _dbContext.LetterRequest.Count();
        }
        public int GetEmployeeLetterRequestsCount(int userId)
        {
            return _dbContext.LetterRequest.Where(L => L.UserId.Equals(userId)).Count();
        }
    
        public IList<LetterRequests> getAllUserLetters(int userId)
        {
            IList<LetterRequests> EmployeeLetters = _dbContext.LetterRequest.AsNoTracking().Where(L => L.UserId.Equals(userId)).ToList();
            for (int i = 0; i < EmployeeLetters.Count; ++i)
                EmployeeLetters[i] = AttachLetterType(EmployeeLetters[i]);         
            return EmployeeLetters;
        }
        public IList<LetterRequests> GetPaginatedEmployeeLetterRequests(int userId, int page=1,string sort="Id",string search="",int limit = 10, string SortOrder = "false")
        {
            IQueryable<LetterRequests> letterRequests = _dbContext.LetterRequest.Where(L => L.UserId.Equals(userId));
            return getPaginatedLetterRequests(letterRequests, page, sort, search, limit, SortOrder);
        }

        private IList<LetterRequests> getPaginatedLetterRequests(IQueryable<LetterRequests> letterList, int page =1,string sort="TypeId",string search="",int limit = 10, string SortOrder = "false")
        {          
            letterList.Include(e => e.LetterType).Load();
            if (search != "")           
                letterList = letterList.Where(L => L.LetterType.Type.Contains(search) 
                /* Since Type as title, is included in list
                */|| L.UserId.ToString().Contains(search) || L.LetterTypeId.ToString().Contains(search) || L.Status.Contains(search) || L.Urgency.Contains(search));
            //Search not Implemented on Information attribute. 
            if(SortOrder == "false")
            {
                if (sort == "Type")
                    letterList = letterList.OrderBy(L => EF.Property<object>(L.LetterType, sort));
                else
     
                    letterList = letterList.OrderBy(L => EF.Property<object>(L, sort));
            }
            else
            {
                if (sort == "Type")
                    letterList = letterList.OrderByDescending(L => EF.Property<object>(L.LetterType, sort));
                else

                    letterList = letterList.OrderByDescending(L => EF.Property<object>(L, sort));
            }
           
            IList<LetterRequests> paginatedRequests= letterList.Skip((page-1)*limit).Take(limit).ToList();          
            return paginatedRequests;
        }
        public LetterRequests getSingleLetterRequest(int id)
        {
            LetterRequests EmployeeLetter = _dbContext.LetterRequest.AsNoTracking().FirstOrDefault(L => L.Id.Equals(id));
           
            //  LetterRequests EmployeeLetter = _dbContext.LetterRequest.Find(id);
            return AttachLetterType(EmployeeLetter);
        }

        public IEnumerable<LetterRequests> GetAllLetterRequests(int page, string sort, string search, int limit, string SortOrder)
        {
            IQueryable<LetterRequests> EmployeeLetters = _dbContext.LetterRequest;

            foreach (LetterRequests Emp in EmployeeLetters)
            {
                var name = _dbContext.Employees.Where(e => (e.Id == Emp.UserId)).Select(e => e.PersonalInfo.FullName).FirstOrDefault();
                if (name != null)
                    Emp.EmployeeName = name.ToString();
                else
                    Emp.EmployeeName = null;
            }

            EmployeeLetters.Include(e => e.LetterType).Load();
            if (search != "")
            {
                EmployeeLetters = EmployeeLetters.Where(L => L.LetterType.Type.Contains(search) || L.UserId.ToString().Contains(search) || L.LetterTypeId.ToString().Contains(search) || L.Status.Contains(search) || L.Urgency.Contains(search) || L.EmployeeName.Contains(search));
            }
            if (SortOrder == "false")
            {
                if (sort == "Type")
                    EmployeeLetters = EmployeeLetters.OrderBy(L => EF.Property<object>(L.LetterType, sort));
                else
                    EmployeeLetters = EmployeeLetters.OrderBy(L => EF.Property<object>(L, sort));
            }
            else
            {
                if (sort == "Type")
                    EmployeeLetters = EmployeeLetters.OrderByDescending(L => EF.Property<object>(L.LetterType, sort));
                else
                    EmployeeLetters = EmployeeLetters.OrderByDescending(L => EF.Property<object>(L, sort));
            }

            IList<LetterRequests> paginatedRequests = EmployeeLetters.Skip((page - 1) * limit).Take(limit).ToList();
            List<LetterRequests> CompleteLetterRequestList = new List<LetterRequests>();
            foreach (LetterRequests letter in paginatedRequests)
            {
                CompleteLetterRequestList.Add(AttachLetterType(letter));
            }
            return CompleteLetterRequestList;
        }
        private IQueryable<LetterRequests> ApplyFiltering(IQueryable<LetterRequests> query, LetterRequestsQuery queryObj)
        {
            // Wild search
            if (!string.IsNullOrWhiteSpace(queryObj.Search))
                query = query.Where(a => a.DateCreated.ToString().Contains(queryObj.Search)
                                         || a.EmployeeName.Contains(queryObj.Search)
                                         || a.Status.Contains(queryObj.Search)
                                         || a.Urgency.Contains(queryObj.Search)
                                         || a.LetterType.Type.Contains(queryObj.Search)
                                         || a.UserId.ToString().Contains(queryObj.Search));

            if (!string.IsNullOrWhiteSpace(queryObj.Status))
                query = query.Where(v => v.Status.Contains(queryObj.Status));

            if (!string.IsNullOrWhiteSpace(queryObj.LetterType))
                query = query.Where(v => v.LetterType.Type.Contains(queryObj.LetterType));

            if (queryObj.UserId.HasValue)
                query = query.Where(v => v.UserId.Equals(queryObj.UserId.Value));

            return query;
        }
        private IQueryable<LetterRequests> GetQueryable()
        {
            return _dbContext.LetterRequest.Include(letterRequests=>letterRequests.LetterType);
        }

        public async Task<QueryResult<LetterRequests>> GetLetterRequests(LetterRequestsQuery queryObj)
        {
            var result = new QueryResult<LetterRequests>();
            var query = GetQueryable();
            query = ApplyFiltering(query, queryObj);
            var columnsMap = new Dictionary<string, Expression<Func<LetterRequests, object>>>()
            {
                ["status"] = letterRequest=>letterRequest.Status,
                ["urgency"] = letterRequest => letterRequest.Urgency,
                ["employeeName"] = letterRequest => letterRequest.EmployeeName,
                ["type"] = letterRequest => letterRequest.LetterType.Type,
                ["date"] = letterRequest => letterRequest.DateCreated,
                ["userId"] = letterRequest => letterRequest.UserId,


            };
            query = _queryableExtensions.ApplyOrderingColumnMap(query, queryObj,columnsMap);
            result.TotalItems = await query.CountAsync();
            query = _queryableExtensions.ApplyPaging(query, queryObj);
            result.Items = await query.ToListAsync();
            return result;
        }


        public async Task<bool> PostLetterRequest(LetterRequests letter)
        {
            try
            {

                letter.DateCreated = LocalTimeService.ToLocalTime();
                letter.DateModified = LocalTimeService.ToLocalTime();

                if (letter.UserId > 0)
                {
                    _dbContext.LetterRequest.Add(letter);
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
    }

}

