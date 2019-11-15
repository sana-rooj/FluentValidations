using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public class GrievanceService:Repository<Grievance>,IGrievance
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;

        public GrievanceService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
        public void UpdateMessages(Grievance newGrievance)
        {
            
            var grievances = _dbContext.GrievanceMessage.AsNoTracking().Where(s => s.GrievanceId.Equals(newGrievance.Id)).ToList();
            _dbContext.GrievanceMessage.RemoveRange(grievances);
            newGrievance.Messages.LastOrDefault().TimeStamp = DateTime.Now;
            _dbContext.Grievances.Update(newGrievance);
            _dbContext.SaveChanges();
            
        }
        //changes
        public Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequestsGeneric(int UserId, int page, string sort, string search, int limit, string SortOrder,bool viewMine)

        {
            Tuple<int, IEnumerable<Grievance>> results;

            IQueryable<Employee> emp = _dbContext.Employees.AsNoTracking().Where(p=>p.Id.Equals(UserId));
            IQueryable<int> Role = emp.Select(p => p.RoleId);

            //for employee 
            if(Role.Single().Equals(1)){
               results =  GetPaginatedGrievanceRequestsEmployee(UserId, page, sort, search, limit, SortOrder);
            } else
            {
                results = GetPaginatedGrievanceRequests(Role.Single(), viewMine, UserId, page, sort, search, limit, SortOrder);

            }
            return results;
        }
      
       
        public Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequests(int role, bool viewMine, int userId, int page, string sort, string search, int limit, string SortOrder)

        {

            //Getting types of grievances of current role
            IQueryable<GrievanceTypes> permissionToView = _dbContext.GrievanceType.AsNoTracking().Where(p => p.AssignedTo.Equals(role));
            IQueryable<int> permissionID = permissionToView.Select(p => p.Id);

        //On the basis of types getting the grievances submitted
            IQueryable<Grievance> grievances = _dbContext.Grievances.AsNoTracking().Where(G => permissionID.Contains(G.TypeId) || G.EmployeeId.Equals(userId)).Include(G=>G.Messages);

            if(viewMine == false)
            {
                grievances = _dbContext.Grievances.AsNoTracking().Where(G => permissionID.Contains(G.TypeId)).Include(G => G.Messages);
               

            } else
            {
                grievances = _dbContext.Grievances.AsNoTracking().Where(G => G.EmployeeId.Equals(userId)).Include(G => G.Messages);
               
            }
      
            grievances = grievances.Select(x => new Grievance
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = _dbContext.Employees.Where(a => a.Id == x.EmployeeId).Select(a => a.PersonalInfo.FullName).FirstOrDefault(),
                Status = x.Status,
                TypeId = x.TypeId,
                Messages = x.Messages,
                DateCreated = x.DateCreated,
                DateModified = x.DateModified,
                GrievanceTitle = _dbContext.GrievanceType.Where(a => a.Id == x.TypeId).Select(a => a.Type).FirstOrDefault(),
            });

        

            var tuple = new Tuple<int, IEnumerable<Grievance>>(0, null);
            tuple = getFilteredData(grievances, page,limit,search, sort, SortOrder);
            return tuple;
         

        }

        public Tuple<int, IEnumerable<Grievance>> getFilteredData(IQueryable<Grievance> ListData,int page, int limit, string search, string sort, string sortOrder)
        {
            var tuple = new Tuple<int, IEnumerable<Grievance>>(0, null);
            IQueryable<Grievance> listData = ListData;
            //search all table Columns
            if (search != "")
                listData = listData.Where(L => L.EmployeeName.Contains(search) || L.GrievanceTitle.Contains(search) || L.Status.Contains(search) || L.Messages.ToList()[0].TimeStamp.ToString().Contains(search));

            IEnumerable<Grievance> RecordsToSort = listData.ToList();
            // Sort on table column
            if (sortOrder == "descending")
            {
                
                RecordsToSort = RecordsToSort.OrderByDescending(i => i.GetType().GetProperty(sort).GetValue(i, null)).ToList();
            }
            else
            {
                RecordsToSort = RecordsToSort.OrderBy(i => i.GetType().GetProperty(sort).GetValue(i, null)).ToList();

            }
            int count = RecordsToSort.Count();
            IQueryable<Grievance> paginatedrequests = RecordsToSort.AsQueryable().Skip((page - 1) * limit).Take(limit);
            tuple = new Tuple<int, IEnumerable<Grievance>>(count, paginatedrequests);
            return tuple;
        }

      public void AddGrievance(Grievance grievanceRequest)
        {
          
          grievanceRequest.DateCreated = LocalTimeService.ToLocalTime();
            grievanceRequest.DateModified = LocalTimeService.ToLocalTime();
            grievanceRequest.Messages.FirstOrDefault().TimeStamp = DateTime.Now;
            _dbContext.Grievances.Add(grievanceRequest);
            _dbContext.SaveChanges();
        }

       

        public Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequestsEmployee(int userId, int page, string sort, string search, int limit, string SortOrder)
        {
            IQueryable<GrievanceTypes> permissionToView = _dbContext.GrievanceType.AsNoTracking().Select(p=>p);
            IQueryable<int> permissionID = permissionToView.Select(p => p.Id);
            IQueryable<Grievance> grievances = _dbContext.Grievances.AsNoTracking().Where(G => G.EmployeeId == userId).Include(G => G.Messages);
            grievances = _dbContext.Grievances.AsNoTracking().Where(G => G.EmployeeId.Equals(userId)).Include(G => G.Messages)
                .Select(x => new Grievance
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = _dbContext.Employees.Where(a => a.Id == x.EmployeeId).Select(a => a.PersonalInfo.FullName).FirstOrDefault(),
                    Status = x.Status,
                    TypeId = x.TypeId,
                    Messages = x.Messages,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    GrievanceTitle = _dbContext.GrievanceType.Where(a => a.Id == x.TypeId).Select(a => a.Type).FirstOrDefault(),
                });
            var tuple = new Tuple<int, IEnumerable<Grievance>>(0, null);
            tuple = getFilteredData(grievances, page, limit, search, sort, SortOrder);
            return tuple;


        }
        public Grievance GetbyId(int id)
        {
           IQueryable<Grievance> temp = _dbContext.Grievances.Where(G => G.Id == id).Include(G=>G.Messages);
            return temp.ToList()[0];
        }

        public int getTotalPageCountByUserIdwithSearch(int Id, string search)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPageCountGeneric(int UserId)
        {
            throw new NotImplementedException();
        }

        public int getSearchPageCountGeneric(int UserId, string search)
        {
            throw new NotImplementedException();
        }
    }
}
