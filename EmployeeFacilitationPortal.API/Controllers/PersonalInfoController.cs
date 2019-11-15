using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class PersonalInfoController : ControllerBase
    {
        private readonly IPersonalInfo _personalInfoService;
        private readonly ILogService _logger;
      
        public PersonalInfoController(IPersonalInfo personalInfoService, ILogService log)
        {
            _personalInfoService = personalInfoService;
            _logger = log;
        }
        //GET: api/Employee
        [HttpGet]
        public async Task<IEnumerable<PersonalInfo>> GetPersonalInfos()
        {
            return await _personalInfoService.GetAll();
        }

        // GET: api/PersonalInfo/username/5
        [HttpGet("username/{email}")]
        public IActionResult GetByEmail([FromRoute]string email)
        {
            PersonalInfo personalInfo;
            try
            {
                //string email = "fahadameen@gmail.com";
                personalInfo = _personalInfoService.GetByEmail(email);
                if (personalInfo == null)
                    return NotFound("Record Not Found.");
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured."); 
            }

            return Ok(personalInfo);
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody] PersonalInfo personalInfo)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }
            try
            {
                _personalInfoService.Add(personalInfo);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
        //[HttpGet("download/{id}")]
            //public  Task<IActionResult> Download([FromRoute] int id)
            //{
        // GET: api/Employee/username/5
        [HttpGet("previewImage/{username}")]
        public IActionResult GetImage(string username)
        {
            string ImageAsBase64 = "";
            try
            {
                ImageAsBase64 = _personalInfoService.PreviewProfileImage(username);                
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured."); 
            }
            return Ok(ImageAsBase64);
        }
     }
}
