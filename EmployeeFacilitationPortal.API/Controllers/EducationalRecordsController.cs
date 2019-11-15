using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalRecordsController : ControllerBase
    {
        private readonly DBContext _context;

        public EducationalRecordsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/EducationalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationalRecord>>> GetEducationalRecords()
        {
            return await _context.EducationalRecords.ToListAsync();
        }

        // GET: api/EducationalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalRecord>> GetEducationalRecord(int id)
        {
            var educationalRecord = await _context.EducationalRecords.FindAsync(id);

            if (educationalRecord == null)
            {
                return NotFound();
            }

            return educationalRecord;
        }

        // PUT: api/EducationalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationalRecord(int id, EducationalRecord educationalRecord)
        {
            if (id != educationalRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(educationalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationalRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EducationalRecords
        [HttpPost]
        public async Task<ActionResult<EducationalRecord>> PostEducationalRecord(EducationalRecord educationalRecord)
        {
            _context.EducationalRecords.Add(educationalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducationalRecord", new { id = educationalRecord.Id }, educationalRecord);
        }

        // DELETE: api/EducationalRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EducationalRecord>> DeleteEducationalRecord(int id)
        {
            var educationalRecord = await _context.EducationalRecords.FindAsync(id);
            if (educationalRecord == null)
            {
                return NotFound();
            }

            _context.EducationalRecords.Remove(educationalRecord);
            await _context.SaveChangesAsync();

            return educationalRecord;
        }

        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllEducationalRecords(int employeeId)
        {
            var records = _context.EducationalRecords.Where(record => record.EmployeeId == employeeId);
            if (records == null)
            {
                return NotFound();
            }

            foreach (EducationalRecord current in records)
            {
                _context.EducationalRecords.Remove(current);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        private bool EducationalRecordExists(int id)
        {
            return _context.EducationalRecords.Any(e => e.Id == id);
        }
    }
}
