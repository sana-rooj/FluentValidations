using System;
using System.Linq;
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
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IPasswordResetService _reset;
        private readonly ILogService _logger;

        public PasswordResetController(IPasswordResetService reset,ILogService _log)
        {
            _reset = reset;
            _logger = _log;

        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("reset")]
        public void  Generate_Link([FromBody]string email)
        {
            string S = email;
            try
            {
                _reset.generate_link(email);
            }
            catch(Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
              //  return StatusCode(StatusCodes.Status400BadRequest);
            }
            
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public PasswordReset GetObjOnEmpId(string userEmail)
        {
            return _reset.GetObjUsingOfficialEmail(userEmail);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("check_validity")]
        public bool Check_Timeout(string email)
        {
            return _reset.check_timeout(email);
        }

        [AllowAnonymous]
        [HttpPut("resetpassword")]
        public IActionResult ResetPassword([FromBody]Login login)
        {
            try
            {
                _reset.PasswordReset(login);
                
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception E)
            {
                //Console.Write(E);
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }

    }
}