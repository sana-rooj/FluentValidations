using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using EmployeeFacilitationPortal.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EmployeeFacilitationPortal.Services.Filters;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class GrievanceController : ControllerBase
    {
        private readonly IGrievanceTypes _grievanceTypeService;
        private readonly ILogService _logger;
        private readonly IGrievance _grevianceSubmissionService;
        private readonly IEmployee _employeeService;
        public GrievanceController(IGrievanceTypes grievance, ILogService _log, IGrievance grievanceSubmission,IEmployee employee)
        {
            _grievanceTypeService = grievance;
            _logger = _log;
            _grevianceSubmissionService = grievanceSubmission;
            _employeeService = employee;
           
        }


        //[EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<IActionResult>  GetGrievanceTypes()
        {
            IEnumerable<GrievanceTypes> AllGrievanceTypes = new List<GrievanceTypes>();
            try
            {
                AllGrievanceTypes = await _grievanceTypeService.GetAll();
                if (AllGrievanceTypes == null)
                    return NotFound($"No Record Found");

            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.ToString(), ex.GetType().ToString());
                return NotFound("Exception Occured.");
            }

            return Ok(AllGrievanceTypes);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("postGrievance")]
        public IActionResult PostGrievance([FromBody]Grievance grievanceRequest)
        {
            
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }

            try
            {
       
                _grevianceSubmissionService.AddGrievance(grievanceRequest);

            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return StatusCode(StatusCodes.Status201Created);


        }
 
        [Permissions("Grievance/List")]
        [HttpGet("GetAllListOfGrievances")]
        public IActionResult GetGrievanceList(bool viewMine, int userId, int page = 1, string sort = "EmployeeName", string search = "", int limit = 1, string SortOrder = "ascending")
        {
          
            var tuple = new Tuple<int, IEnumerable<Grievance>>(0,null);
            try
            {
              tuple =  _grevianceSubmissionService.GetPaginatedGrievanceRequestsGeneric(userId, page, sort, search, limit, SortOrder, viewMine);
                if (tuple.Item2 == null)
                    return NotFound($"No Record Found");

            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.ToString(), ex.GetType().ToString());
                return NotFound("Exception Occured.");
            }

            
            return Ok(tuple);

        }
      
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Grievance Grievance;
            try
            {
                Grievance = _grevianceSubmissionService.GetbyId(id);
                if (Grievance == null)
                    return NotFound("Record Not Found.");
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }

            return Ok(Grievance);

        }
           
        [HttpPut]
        public IActionResult Put(Grievance newGrievance)
        {
            try
            {
               _grevianceSubmissionService.UpdateMessages(newGrievance);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(newGrievance);
        }

    }
}
