using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IGrievance: IRepository<Grievance>
    {
     
      
        Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequestsEmployee(int userId, int page, string sort, string search, int limit, string SortOrder);
        Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequests(int role, bool viewMine, int userId, int page = 1, string sort = "", string search = "", int limit = 10, string SortOrder = "ascending");
        Grievance GetbyId(int id);
        //int getTotalPageCountByRolewithSearch(string role, string search);
        int getTotalPageCountByUserIdwithSearch(int Id, string search);

        int GetTotalPageCountGeneric(int UserId);
        int getSearchPageCountGeneric(int UserId, string search);
        void UpdateMessages(Grievance newGrievance);
        Tuple<int, IEnumerable<Grievance>> GetPaginatedGrievanceRequestsGeneric(int UserId, int page, string sort, string search, int limit, string SortOrder,bool viewMine);
        void AddGrievance(Grievance grievanceRequest);


    }
}
