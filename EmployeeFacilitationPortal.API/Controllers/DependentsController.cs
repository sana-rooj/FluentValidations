using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DependentsController : ControllerBase
    {
        private readonly DBContext _context;

        public DependentsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Dependents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetDependents()
        {
            return await _context.Dependents.ToListAsync();
        }

        // GET: api/Dependents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dependent>> GetDependent(int id)
        {
            var dependent = await _context.Dependents.FindAsync(id);

            if (dependent == null)
            {
                return NotFound();
            }

            return dependent;
        }

        // PUT: api/Dependents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependent(int id, Dependent dependent)
        {
            if (id != dependent.Id)
            {
                return BadRequest();
            }

            _context.Entry(dependent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependentExists(id))
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

        // POST: api/Dependents
        [HttpPost]
        public async Task<ActionResult<Dependent>> PostDependent(Dependent dependent)
        {
            _context.Dependents.Add(dependent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependent", new { id = dependent.Id }, dependent);
        }


        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteDependents(int employeeId)
        {
            var dependents = _context.Dependents.Where(dependent => dependent.EmployeeId == employeeId);

            if (dependents == null)
            {
                return NotFound();
            }
            var dependentsList = dependents.ToList();
            if (dependentsList == null || dependentsList.Count == 0)
            {
                return Ok(); // there were no records to be deleted
            }

            foreach (Dependent currentDependent in dependents)
            {
                _context.Dependents.Remove(currentDependent);
            }
            
            await _context.SaveChangesAsync();

            return Ok();
        }


        // DELETE: api/Dependents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dependent>> DeleteDependent(int id)
        {
            var dependent = await _context.Dependents.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }

            _context.Dependents.Remove(dependent);
            await _context.SaveChangesAsync();

            return dependent;
        }

      
        private bool DependentExists(int id)
        {
            return _context.Dependents.Any(e => e.Id == id);
        }
    }
}
