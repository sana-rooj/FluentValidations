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
    public class WorkHistoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public WorkHistoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/WorkHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkHistory>>> GetWorkHistories()
        {
            return await _context.WorkHistories.ToListAsync();
        }

        // GET: api/WorkHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkHistory>> GetWorkHistory(int id)
        {
            var workHistory = await _context.WorkHistories.FindAsync(id);

            if (workHistory == null)
            {
                return NotFound();
            }

            return workHistory;
        }

        // PUT: api/WorkHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkHistory(int id, WorkHistory workHistory)
        {
            if (id != workHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(workHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkHistoryExists(id))
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

        // POST: api/WorkHistories
        [HttpPost]
        public async Task<ActionResult<WorkHistory>> PostWorkHistory(WorkHistory workHistory)
        {
            _context.WorkHistories.Add(workHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkHistory", new { id = workHistory.Id }, workHistory);
        }

        // DELETE: api/WorkHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkHistory>> DeleteWorkHistory(int id)
        {
            var workHistory = await _context.WorkHistories.FindAsync(id);
            if (workHistory == null)
            {
                return NotFound();
            }

            _context.WorkHistories.Remove(workHistory);
            await _context.SaveChangesAsync();

            return workHistory;
        }


        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllWorkHistories(int employeeId)
        {
            var workHistories = _context.WorkHistories.Where(workHistory => workHistory.EmployeeId == employeeId);
            if (workHistories == null)
            {
                return NotFound();
            }

            foreach (WorkHistory current in workHistories)
            {
                _context.WorkHistories.Remove(current);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool WorkHistoryExists(int id)
        {
            return _context.WorkHistories.Any(e => e.Id == id);
        }
    }
}
