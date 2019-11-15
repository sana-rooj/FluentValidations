using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.ModelQueries;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Filters;
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
    public class LetterRequestsController : ControllerBase
    {
        private readonly ILetterRequests _letterRequestService;
        private readonly ILogService _logger;
        private readonly ILetterTypes _letterTypes;
        private readonly IEmployee _employeeService;
        public LetterRequestsController(ILetterRequests letterRequests, ILogService _log,ILetterTypes letterType,IEmployee employeeS)
        {
            _letterRequestService = letterRequests;
            _logger = _log;
            _letterTypes = letterType;
            _employeeService = employeeS;
        }
     
        [HttpPost]
        public async Task<ActionResult<LetterRequests>> PostUserLetterRequest([FromBody] LetterRequests letter)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }

            try
            {
                letter.DateCreated = System.DateTime.Now;
                letter.DateModified = System.DateTime.Now;
                await _letterRequestService.PostLetterRequest(letter);


            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("GetEmployeeLetters/{empId}")]
        public IActionResult GetEmployeeLettersRequests(int empId)
        {
            try
            {
                IList<LetterRequests> EmployeeLetters = _letterRequestService.getAllUserLetters(empId);
                if (EmployeeLetters == null)
                    return NotFound($"No Letter Exists for Id: {empId}");
                return Ok(EmployeeLetters);

            }catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }

        }
        [HttpGet("types")]
        public async Task<IActionResult> GetTypesAsync()
        {
            List<string> types = new List<string>();
            try
            {
                var letterTypes = await  _letterTypes.GetAll();
                foreach (LetterTypes letter in letterTypes)
                {
                    types.Add(letter.Type);
                }
                if (types == null)
                    return NotFound($"No Record Found");

            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.ToString(), ex.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(types);
        }
        [HttpGet("description")]
        public async Task<IActionResult> GetDescriptions()
        {
            IEnumerable<LetterTypes> letterTypes = null;
            try
            {
                letterTypes =await _letterTypes.GetAll();
                if (letterTypes == null)
                    return NotFound($"No Record Found");

            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.ToString(), ex.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(letterTypes);

        }
        // An Example: api/LetterRequests/GetPaginatedEmployeeLetterRequests/1?page=1&sort=Id
        [HttpGet("GetPaginatedEmployeeLetterRequests/{empId}")]
        public IActionResult GetPaginatedEmployeeLetterRequests(int empId,int page=1, string sort="Id",string search="",int limit =10, string SortOrder = "false")
        {
            try
            {
                IList<LetterRequests> EmployeeLetters = _letterRequestService.GetPaginatedEmployeeLetterRequests(empId, page, sort, search, limit, SortOrder);
                if (EmployeeLetters == null)
                    return NotFound($"No Letter Exists for Id: {empId}");
                return Ok(EmployeeLetters);

            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }

        }   
        [HttpGet("GetTotalCount")]
        public int GetTotalCount()
        {
            return _letterRequestService.GetTotalCount();
        }
        [HttpGet("GetEmployeeLetterRequestsCount/{userId}")]
        public int GetEmployeeLetterRequestsCount(int userId)
        {
            return _letterRequestService.GetEmployeeLetterRequestsCount(userId);           
        }

        [HttpPut( "update/{id}")]
        public IActionResult PutLetterInformation( int id, [FromBody] LetterRequests letter)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }
       
            try
            {
                _letterRequestService.Update(letter);

            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return Ok($"LetterRequest {letter.Id} Updated.");

        }

        [HttpGet("GetSingleEmployeeLetter/{id}")]
        public IActionResult GetSignleEmployeeLetter(int id)
        {
            try
            {
                LetterRequests EmployeeLetter = _letterRequestService.getSingleLetterRequest(id);
                if (EmployeeLetter == null)
                    return NotFound($"No Letter Exists for Id: {id}");
                return Ok(EmployeeLetter);

            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }
        }
        [HttpGet("GetAllLetterRequests")]
        public IActionResult GetAllLetterRequest(int page = 1, string sort = "Id", string search = "", int limit = 10, string SortOrder = "false")
        {
            IEnumerable<LetterRequests> letterRequests = null;
            try
            {
                letterRequests = _letterRequestService.GetAllLetterRequests(page, sort, search, limit, SortOrder);
                if (letterRequests == null)
                    return NotFound($"No Record Found");
            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.ToString(), ex.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(letterRequests);
        }
        [HttpGet("GetId/{userName}")]
        public int GetIdByUserName(string userName)
        {
            var employee = _employeeService.GetByUsername(userName);
            return employee.Id;
        }

        [Permissions("Letter/List")]
        [HttpGet]
        public async Task<ActionResult> GetLetterRequests([FromQuery]LetterRequestsQuery queryObj)
        {
            var queryResult = await _letterRequestService.GetLetterRequests(queryObj);

            return Ok(queryResult);
        }

    }
}
        
       
