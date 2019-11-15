using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _log;

        public LogController(ILogService log)
        {
            _log = log;
        }
        //Random Comment
        // GET: api/Log
        [HttpGet]
        public async Task<IEnumerable<Log>> Get()
        {
            return await _log.GetAll();
        }

        [HttpGet("GetAll")]
        public (IList<Log>,int) Get(int page = 1, DateTime? startDate = null, DateTime? endDate = null, int limit = 50)
        {
           
            try
            {
                if (page < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                
                
            }
            catch (Exception E)
            {
                _log.AddLog(E.ToString(), E.GetType().ToString());
              
            }
            return _log.GetAllLogs(page=1, startDate, endDate, limit);
            //Note: Everything returned by controller has to be an ActionResult.
        }

        [HttpGet("TotalPages")]
        public int TotalPages()
        {
            try
            {
                int totalPages = _log.TotalPages();
                return totalPages;
            }
            catch(Exception E)
            {
                _log.AddLog(E.ToString(), E.GetType().ToString());
                return 0;
            }     
        }
        // POST: api/Log
        [HttpPost]
        public IActionResult Post(Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _log.Add(log);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}