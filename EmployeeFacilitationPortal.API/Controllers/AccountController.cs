using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services;
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
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRegister _registerEmployee;
        private readonly IPersonalInfo _personalInfo;
        private readonly ILoginService _loginService;
        private readonly ILogService _logger;
        private readonly IEmployee _employeeService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly IGenerateWebToken _tokenGeneration;

        public AccountController(IRegister registerEmployee, IEmployee employeeService,
            IPersonalInfo personalInfo, ILoginService loginService, ILogService _Logger,
            IPasswordResetService passwordResetService, IGenerateWebToken tokenGeneration)
        {
            _tokenGeneration = tokenGeneration; // for authorization
            _registerEmployee = registerEmployee;
            _loginService = loginService;
            _logger = _Logger;
            _passwordResetService = passwordResetService;
            _personalInfo = personalInfo;
            _employeeService = employeeService;
        }



     
        [HttpPost("Register")]

        public IActionResult Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }
            try
            {
                var doesExistAlready = _registerEmployee.IsEmpIdExist(employee.EmpId);
                if (doesExistAlready)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Employee ID exists");
                    
                }
               
                //Inside this post with image we are checking if this user.username (containing official email) already exists or not
                if (_registerEmployee.PostWithImage(employee) == true)
                {                                     
                    _passwordResetService.GenerateAndSendEmail(employee.PersonalInfo.Email, employee.Username);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
 
       

        [AllowAnonymous]
        [HttpPost("Login/checkLogin")]
        public async Task<IActionResult> CheckLoginAsync([FromBody] Login login)
        {
            if (!ModelState.IsValid) {
                _logger.AddLog(BadRequest(ModelState).ToString(),BadRequest().GetType().ToString() );

                return BadRequest(ModelState);
            }
            try
            {
                if (_loginService.VerifyEmail(login.Email, login.Password))
                {
                    Employee employee = await _employeeService.GetByUsername(login.Email);

                    if (employee == null)
                    {
                        return NotFound("Record not found");
                    }
                    //======= Authorization =======//
                    // while generating token, we attach role as a claim which is then utilized in PermissionAuthorizationHandler
                    var tokenString = _tokenGeneration.GenerateJSONWebToken(employee);

                    //============================//
                    return Ok(new { token = tokenString });
                }
               
            }
            catch (Exception E)
            {
               // _logger.AddLog(NotFound($"LoginFailed: {login.Email},{login.Password}").ToString(), NotFound().GetType().ToString());
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
                                  
            }
            return NotFound("Email or Pass do not match");

        }

        [HttpPut("Login/{id}")]
        public IActionResult Put([FromRoute] int id,[FromBody] Login login)
        {
            if (!ModelState.IsValid) {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }

            if (id != login.Id) {
                _logger.AddLog(BadRequest("Login Id does not Match.").ToString(),BadRequest().GetType().ToString());
                return BadRequest();
            }

            try
            {
                _loginService.Update(login);
            }
            catch (Exception E)
            {
                //_logger.AddLog(NotFound($"Update Failed, No record found against the Id: {id}").ToString(),NotFound().GetType().ToString());
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok($"Login {login.Id} Updated.");
        }
       
        // DELETE: api/Samples/5
        [HttpDelete("Login/{id}")]
        public IActionResult Delete([FromBody] Login login)
        {
            try
            {
                _loginService.Remove(login);
            }catch (Exception E)
            {
                // _logger.AddLog("Login Info didnot match, Delete Failed", E.GetType().ToString());
                _logger.AddLog(E.ToString(),E.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok($"Login {login.Id} Deleted.");
        }
    }
}