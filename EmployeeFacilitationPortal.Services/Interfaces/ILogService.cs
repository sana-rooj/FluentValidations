using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface ILogService : IRepository<Log>
    {
        IEnumerable<Log> GetLogs();
        //void AddLog(Log log);
        void AddLog(string log,string type);
        (IList<Log>, int) GetAllLogs(int page, DateTime? startDate, DateTime? endDate, int limit);
        int TotalPages();


    }
}
