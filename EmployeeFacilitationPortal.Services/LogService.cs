using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class LogService : Repository<Log>, ILogService
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;

        public LogService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }

        public string GetStackTrace()
        {
            string Trace = "";
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            for (int i = 2; i < stackTrace.FrameCount; ++i)
            {
                System.Diagnostics.StackFrame frame = stackTrace.GetFrame(i);
                MethodBase callerMethod = frame.GetMethod();
                Trace += callerMethod + " <= ";

            }

            return Trace;
        }

        public void AddLog(string log, string type)
        {
            Log _Log = new Log
            {
                Message = log,
                Timestamp = System.DateTime.Now, //Can be UTC as well
                Type = type,
                StackTrace = GetStackTrace()
            };

            _dbContext.Logs.Add(_Log);
            //This line should not be here.
            _dbContext.SaveChanges();
        }
        
        public (IList<Log>,int) GetAllLogs(int page = 1, DateTime? startDate = null, DateTime? endDate = null, int limit = 50)
        {
            
            var skip = (page -1) * limit;
            IQueryable<Log> logList;
            logList = from s in _dbContext.Logs select s;
            if (startDate == null)
            {
                startDate = new DateTime(2099, 12, 12); 
            }
            if (endDate == null)
            {
                endDate = new DateTime(1970, 1, 1);
            }
            logList = _dbContext.Logs.Where(p=>p.Timestamp>endDate && p.Timestamp<startDate );
            return (logList.Skip(skip).Take(limit).ToList(), (int)Math.Ceiling(_dbContext.Logs.Count() / 50.0));
        }

        public int TotalPages()
        {
            return (int)Math.Ceiling(_dbContext.Logs.Count()/50.0);
        }

        IEnumerable<Log> ILogService.GetLogs()
        {
            throw new NotImplementedException();
        }
    }
}
