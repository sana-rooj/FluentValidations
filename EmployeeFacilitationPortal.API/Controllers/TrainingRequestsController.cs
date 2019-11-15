using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using EmployeeFacilitationPortal.Services.Filters;

namespace EmployeeFacilitationPortal.API.Controllers
{

    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TrainingRequestsController : ControllerBase
    {
        private readonly ITrainingRequestService _trainingRequestService;
        private readonly ILogService _log;


        public TrainingRequestsController(ITrainingRequestService trainingRequestService, ILogService log)
        {
            _trainingRequestService = trainingRequestService;
            _log = log;
        }

        [HttpGet("types")]
        public IList<TrainingRequestType> getTrainingRequestTypes()
        {

            return _trainingRequestService.GetTrainingRequestTypes();
        } // return all types of trainings possible so that drop down of training request type can be populated 
        // GET: api/TrainingRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingRequest>> GetTrainingRequest(int id)
        {
            var trainingRequest = await _trainingRequestService.GetAllTrainingRequestsWithId(id);

            if (trainingRequest == null)
            {
                return NotFound();
            }

            return Ok(trainingRequest);
        }

        // PUT: api/TrainingRequests/5
        [HttpPut("{id}")]
        public IActionResult PutTrainingRequest(int id, TrainingRequest trainingRequest)
        {
            if (trainingRequest != null && id != trainingRequest.Id)
            {
                return BadRequest();
            }

            try
            {
                this._trainingRequestService.UpdateRecord(trainingRequest);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        // POST: api/TrainingRequests  -- Updated using service
        [HttpPost]
        public async Task<ActionResult<TrainingRequest>> PostTrainingRequest(TrainingRequest trainingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _trainingRequestService.PostTrainingRequest(trainingRequest);
            }
            catch (Exception)
            {
                //AddLog(exception);
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/TrainingRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingRequest>> DeleteTrainingRequest(int id)
        {
            var isDeleted = await _trainingRequestService.RemoveById(id);
            if (isDeleted == null) return NotFound();
            return Ok(isDeleted);
        }

        [Permissions("Training/List")]
        [HttpGet("GetPaginatedEmployeeTrainingRequests/{empId}")]
        public IActionResult GetPaginatedEmployeeLetterRequests(int empId, int page = 1, string sort = "Id", string search = "", int limit = 10, bool sortOrder = false, string searchInColumn = "none", string thenSearchFor = "")

        {
            // EmpID less than 1 will result in returning all values
            try
            {
                IList<TrainingRequest> employeeTrainingRequests = _trainingRequestService.GetPaginatedEmployeeTrainingRequests(empId, page, sort, search, limit, sortOrder, searchInColumn, thenSearchFor);
                if (employeeTrainingRequests == null)
                    return NotFound($"No Training Exists for employee Id: {empId}");
                return Ok(employeeTrainingRequests);

            }
            catch (Exception E)
            {
                _log.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound(E.ToString());
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetLetterRequests([FromQuery]TrainingRequestsQuery queryObj)
        {
            var queryResult = await _trainingRequestService.GetTrainingRequests(queryObj);

            return Ok(queryResult);
        }
    }
}
