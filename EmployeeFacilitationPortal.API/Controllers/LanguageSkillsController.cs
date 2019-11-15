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
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]

    public class LanguageSkillsController : ControllerBase
    {
        private readonly DBContext _context;

        public LanguageSkillsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/LanguageSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageSkill>>> GetLanguageSkills()
        {
            return await _context.LanguageSkills.ToListAsync();
        }

        // GET: api/LanguageSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageSkill>> GetLanguageSkill(int id)
        {
            var languageSkill = await _context.LanguageSkills.FindAsync(id);

            if (languageSkill == null)
            {
                return NotFound();
            }

            return languageSkill;
        }

        // PUT: api/LanguageSkills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageSkill(int id, LanguageSkill languageSkill)
        {
            if (id != languageSkill.Id)
            {
                return BadRequest();
            }

            _context.Entry(languageSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageSkillExists(id))
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

        // POST: api/LanguageSkills
        [HttpPost]
        public async Task<ActionResult<LanguageSkill>> PostLanguageSkill(LanguageSkill languageSkill)
        {
            _context.LanguageSkills.Add(languageSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguageSkill", new { id = languageSkill.Id }, languageSkill);
        }

        // DELETE: api/LanguageSkills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageSkill>> DeleteLanguageSkill(int id)
        {
            var languageSkill = await _context.LanguageSkills.FindAsync(id);
            if (languageSkill == null)
            {
                return NotFound();
            }

            _context.LanguageSkills.Remove(languageSkill);
            await _context.SaveChangesAsync();

            return languageSkill;
        }

        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllLanguageSkills(int employeeId)
        {
            var languageSkills = _context.LanguageSkills.Where(skill => skill.EmployeeId == employeeId);
            if (languageSkills == null)
            {
                return NotFound();
            }

            foreach (LanguageSkill current in languageSkills)
            {
                _context.LanguageSkills.Remove(current);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LanguageSkillExists(int id)
        {
            return _context.LanguageSkills.Any(e => e.Id == id);
        }
    }
}
