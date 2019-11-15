using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.Entities;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        private readonly ILogService _logger;

        public EmployeeController(IEmployee employeeService, ILogService log)
        {
            _employeeService = employeeService;
            _logger = log;
        }

        //[HttpPost("SendEmailLink")]
        //public IActionResult SendLink([FromBody] Employee user)
        //{
        //    try
        //    {
        //        if (user != null) _employeeService.GenerateLink(user.Username, user.Id, user.PersonalInfo.Email);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.AddLog(e.ToString(), e.GetType().ToString());
        //        return StatusCode(StatusCodes.Status400BadRequest);
        //    }
        //    return Ok();
        //}



        [Permissions("Employee/List")]
        [HttpGet("UnregisteredEmployee")]
        public IActionResult GetUnregisteredemployee(int page = 1, string sort = "Id", bool sortOrder = false, string search = "", int limit = 10)
        {
            IList<Employee> empList = null;
            try
            {
                empList = _employeeService.GetPasswordResetList(page, sort, sortOrder, search, limit);
                if (empList == null)
                {
                    return NotFound("No Record Found.");
                }
            }
            catch (Exception e)
            {
                _logger.AddLog(e.ToString(), e.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(empList); //Note: Everything returned by controller has to be an ActionResult.
        }


        //public IList<Employee> GetUnregisteredemployee()
        //   // return _employeeService.GetPasswordResetList();


        //}

        //public Task <IList<Employee> GetUnregisteredemployee()
        //{
        //    return _employeeService.GetPasswordList();
        //}

        //GET: api/Employee
        //[HttpGet]

        //public async Task<IEnumerable<Employee>>  GetEmployees()
        //{
        //    return await _employeeService.GetAll();
        //}

        // GET: api/Employee/5
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employee = await _employeeService.GetById(id);
                if (employee == null)
                    return NotFound("Record Not Found.");

                return Ok(employee);
            }
            catch (Exception e)
            {
                _logger.AddLog(e.ToString(), e.GetType().ToString());
                return NotFound("Exception Occured.");
            }

        }

        [Permissions("Employee/List")]
        [HttpGet("UnregisteredAccountsCount")]
        public int GetUnregisteredAccountCount()
        {
            return _employeeService.GetUnRegisteredAccountCount();
        }

        [HttpGet("GetRole/{email}")]
        public int GetRole(string email) //@Fahad Please add Exception Handling to this Function
        {
            return _employeeService.GetRole(email);
        }

        // GET: api/Employee/userIdAgainstUsername/rima@ciklum.com
        [HttpGet("userIdAgainstUsername/{username}")]
        public async Task<IActionResult> GetUserId(string username)
        {
            Employee employee;
            try
            {
                employee = await _employeeService.GetByUsername(username);
                if (employee == null)
                    return NotFound("Record Not Found.");
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured."); //Questioning the purpose of this line.
            }

            return Ok(employee.Id);
        }

        // GET: api/Employee/username/5

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            Employee employee;
            try
            {
                employee = await _employeeService.GetByUsername(username);
                if (employee == null)
                    return NotFound("Record Not Found.");
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured."); //Questioning the purpose of this line.
            }

            return Ok(employee);
        }

        
        [HttpPut("updateUser/{username}")]
        public async Task<IActionResult> PutByUsername([FromRoute] string username, [FromBody] Employee updatedEmployee)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());

                return BadRequest(ModelState);
            }


            try
            {
                var employee = await _employeeService.GetByUsername(username);
                if (employee != null)
                {
                    updatedEmployee.Id = employee.Id;
                    _employeeService.EmpUpdate(updatedEmployee, true);
                }
            }
            catch (Exception e)
            {
                _logger.AddLog(e.ToString(), e.GetType().ToString());
                return NotFound(e.ToString());
            }
            return Ok();

        }

        // PUT: api/Employee/5

        
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());

                return BadRequest(ModelState);
            }

            if (employee != null && id != employee.Id)
            {
                _logger.AddLog(BadRequest("Employee Id does not Match.").ToString(), BadRequest().GetType().ToString());

                return BadRequest();
            }

            try
            {
                _employeeService.EmpUpdate(employee);
            }
            catch (Exception e)
            {
                _logger.AddLog(e.ToString(), e.GetType().ToString());
                return NotFound("No Record Found against this Id.");
            }
            return Ok();
        }
        
        [HttpPut("UpdateAfterDeletion/{id}")]
        public IActionResult PutAfterRemovingPreviousObject([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());

                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                _logger.AddLog(BadRequest("Employee Id does not Match.").ToString(), BadRequest().GetType().ToString());

                return BadRequest();
            }

            try
            {
                _employeeService.EmpUpdate(employee, true);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("No Record Found against this Id.");
            }
            return Ok();
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeService.Add(employee);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] Employee employee)
        {
            try
            {
                if (employee != null) await _employeeService.Remove(employee);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("The Record to be Deleted does not Exist/Exception Occured.");
            }

            return Ok($"Employee {employee.Id} Deleted.");
        }

        [Permissions("Employee/List")]
        [HttpGet]
        public async Task<ActionResult> GetEmployees([FromQuery]EmployeeQuery queryObj)
        {
            var queryResult = await _employeeService.GetEmployees(queryObj);

            return Ok(queryResult);
        }

    }
}