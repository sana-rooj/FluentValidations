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
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
    [ApiController]
    public class ProfessionalReferencesController : ControllerBase
    {
        private readonly DBContext _context;

        public ProfessionalReferencesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProfessionalReferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionalReference>>> GetProfessionalReferences()
        {
            return await _context.ProfessionalReferences.ToListAsync();
        }

        // GET: api/ProfessionalReferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionalReference>> GetProfessionalReference(int id)
        {
            var professionalReference = await _context.ProfessionalReferences.FindAsync(id);

            if (professionalReference == null)
            {
                return NotFound();
            }

            return professionalReference;
        }

        // PUT: api/ProfessionalReferences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionalReference(int id, ProfessionalReference professionalReference)
        {
            if (id != professionalReference.Id)
            {
                return BadRequest();
            }

            _context.Entry(professionalReference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionalReferenceExists(id))
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

        // POST: api/ProfessionalReferences
        [HttpPost]
        public async Task<ActionResult<ProfessionalReference>> PostProfessionalReference(ProfessionalReference professionalReference)
        {
            _context.ProfessionalReferences.Add(professionalReference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessionalReference", new { id = professionalReference.Id }, professionalReference);
        }

        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllReferences(int employeeId)
        {
            var references = _context.ProfessionalReferences.Where(reference => reference.EmployeeId == employeeId);
            if (references == null)
            {
                return NotFound();
            }

            foreach (ProfessionalReference current in references)
            {
                _context.ProfessionalReferences.Remove(current);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        // DELETE: api/ProfessionalReferences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfessionalReference>> DeleteProfessionalReference(int id)
        {
            var professionalReference = await _context.ProfessionalReferences.FindAsync(id);
            if (professionalReference == null)
            {
                return NotFound();
            }

            _context.ProfessionalReferences.Remove(professionalReference);
            await _context.SaveChangesAsync();

            return professionalReference;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ProfessionalReferenceExists(int id)
        {
            return _context.ProfessionalReferences.Any(e => e.Id == id);
        }
    }
}
