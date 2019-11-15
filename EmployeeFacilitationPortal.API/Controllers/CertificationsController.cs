using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("AllowOrigin")]
    public class CertificationsController : ControllerBase
    {
        private readonly ICertification _certficateService;
        private readonly ILogService _logger;
        public CertificationsController(ICertification certification, ILogService log)
        {
            _certficateService = certification;
            _logger = log;
        }

        // GET: api/Certifications
        [HttpGet("GetAllofEmployee/{EmpId}")]
        public IActionResult GetCertifications(int EmpId)
        {
            IEnumerable<Certification> _certificationList = null;
            try
            {
                _certificationList = _certficateService.GetAll(EmpId);
                if (_certificationList == null)
                    return NotFound($"No Record Found against Employee Id: {EmpId}");
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }
            return Ok(_certificationList);
        }

        // GET: api/Certifications/5
        //[HttpGet("GetSpecific/{id}")]
        //public IActionResult GetCertification(int id)
        //{
        //    Certification certification = null;

        //    try
        //    {
        //        certification = _certficateService.Get(id);
        //        if (certification == null)
        //        {
        //            return NotFound($"No Record Found against Id: {id}");
        //        }
        //    }
        //    catch (Exception E)
        //    {
        //        _logger.AddLog(E.ToString(), E.GetType().ToString());
        //        return NotFound("Exception Occured.");
        //    }
        //    return Ok(certification);
        //}

        // PUT: api/Certifications/5
        [HttpPut("{id}")]
        public IActionResult PutCertification([FromRoute]  int id, [FromBody] Certification certification)
        {
            if (id != certification.Id)
            {
                return BadRequest("Id mismatached");
            }

            try
            {
                _certficateService.Update(certification);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                if (!_certficateService.CertificationExists(id))
                {
                    return NotFound($"Id: {id} not found in Certification.");
                }
                return NotFound("Exception Occured.");
            }

            return NoContent();
        }

        // POST: api/Certifications
        [HttpPost]
        public IActionResult PostCertification([FromBody] Certification certification)
        {
            if (!ModelState.IsValid)
            {
                _logger.AddLog(BadRequest(ModelState).ToString(), BadRequest().GetType().ToString());
                return BadRequest(ModelState);
            }

            try
            {
                _certficateService.Add(certification);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return StatusCode(StatusCodes.Status201Created);
            // return CreatedAtAction("GetCertification", new { id = certification.Id }, certification);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        // DELETE: api/Certifications/5
        //[HttpDelete("{id}")] // couldn't figure out how to post body in delete function hence using other one
        public IActionResult DeleteCertification([FromBody] Certification certification)
        {

            try
            {
                _certficateService.Remove(certification);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("The Record to be Deleted does not Exist");
            }
            return Ok($"Certification {certification.Id} Deteled.");
        }

        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllBasicInfoAttachments(int employeeId)
        {
            try
            {
                _certficateService.RemoveAllAgainst(employeeId);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("The Record to be Deleted does not Exist");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Certification>> DeleteCertificate(int id)
        {
            try
            {
              await  _certficateService.Remove(id);
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("The Record to be Deleted does not Exist");
            }
            return Ok();
        }

    }
}