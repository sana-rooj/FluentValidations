using System;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CompanyInformationController : ControllerBase
    {
        private readonly ICompanyInformation _companyInformation;
        private readonly ILogService _log;

        public CompanyInformationController(ICompanyInformation companyInformation, ILogService log)
        {
            _companyInformation = companyInformation;
            _log = log;
        }

        // POST: api/Samples
        [HttpPost("policy")]
        public IActionResult PostPolicy([FromBody] CompanyInformation companyInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _companyInformation.AddPolicy(companyInformation);
            }
            catch (Exception)
            {
                //AddLog(exception);
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return Ok(companyInformation);
        }

        [HttpPost("handbook")]
        public IActionResult PostHandbook([FromBody] CompanyInformation companyInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _companyInformation.AddHandbook(companyInformation);
            }
            catch (Exception)
            {
                //AddLog(exception);
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return Ok(companyInformation);
        }

        [HttpPost("orientation")]
        public IActionResult Post([FromBody] CompanyInformation companyInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _companyInformation.AddOrientation(companyInformation);
            }
            catch (Exception)
            {
                //AddLog(exception);
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return Ok(companyInformation);
        }

        [HttpGet("length")]
        public IActionResult Get()
        {
            var sample = _companyInformation.FileLength(
                "C:\\Users\\mfah\\source\\repos\\EmployeeFacilitationPortal.WebApi\\EmployeeFacilitationPortal.API\\wwwroot\\Policies\\Policy_v2.pdf");
            return Ok(sample);
        }

        [HttpGet]
        public IEnumerable<CompanyInformation> GetAll()
        {
            return _companyInformation.GetActiveCompanyInformations();
        }

        [HttpGet("policy")]
        public IEnumerable<CompanyInformation> GetAllPolicies()
        {
            return _companyInformation.GetPolicies();
        }

        [HttpGet("handbook")]
        public IEnumerable<CompanyInformation> GetAllHandbooks()
        {
            return _companyInformation.GetHandbooks();
        }

        [HttpGet("orientation")]
        public IEnumerable<CompanyInformation> GetAllOrientations()
        {
            return _companyInformation.GetOrientations();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_companyInformation.delete(id))
            {
                return Ok();
            }

            return NotFound("Not Found");
        }

        // GET: api/Samples/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var sample = _companyInformation.Get(id);
            if (sample == null)
            {
                return NotFound("No Record Found...");
            }

            return Ok(sample);
        }

        // PUT: api/Samples/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, CompanyInformation sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sample.Id)
            {
                return BadRequest();
            }

            try
            {
                _companyInformation.Update(sample);
            }
            catch (Exception)
            {
                //AddLog(exception);
                return NotFound("No Record Found against this Id...");
            }

            return Ok("Record Updated...");
        }

        //[HttpGet("download/{id}")]
        //public IActionResult Download([FromRoute] int id)
        //{
        //    var base64 = _companyInformation.DownloadFile(id);
        //    if (base64.Equals(""))
        //    {
        //        return NotFound();
        //    }

        //    return Ok(base64);
        //}
        [HttpGet("download/{id}")]
        public IActionResult Download2([FromRoute] int id)
        {
            var base64 = _companyInformation.DownloadFile(id);
            if (base64.Equals(""))
            {
                return NotFound();
            }

            return Ok(base64);
        }

    }
}