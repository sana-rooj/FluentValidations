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
    public class BasicInfoAttachmentsController : ControllerBase
    {
        private readonly DBContext _context;

        public BasicInfoAttachmentsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/BasicInfoAttachments
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<BasicInfoAttachments>>> GetAllAttachments()
        //{
        //    return await _context.AllAttachments.ToListAsync();
        //}

        //// GET: api/BasicInfoAttachments/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BasicInfoAttachments>> GetBasicInfoAttachments(int id)
        //{
        //    var basicInfoAttachments = await _context.AllAttachments.FindAsync(id);

        //    if (basicInfoAttachments == null)
        //    {
        //        return NotFound();
        //    }

        //    return basicInfoAttachments;
        //}

        //// PUT: api/BasicInfoAttachments/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBasicInfoAttachments(int id, BasicInfoAttachments basicInfoAttachments)
        //{
        //    if (id != basicInfoAttachments.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(basicInfoAttachments).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BasicInfoAttachmentsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/BasicInfoAttachments
        //[HttpPost]
        //public async Task<ActionResult<BasicInfoAttachments>> PostBasicInfoAttachments(BasicInfoAttachments basicInfoAttachments)
        //{
        //    _context.AllAttachments.Add(basicInfoAttachments);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBasicInfoAttachments", new { id = basicInfoAttachments.Id }, basicInfoAttachments);
        //}

        // Delete all dependent records with this employee id 
        [HttpDelete("deleteAll/{employeeId}")]
        public async Task<ActionResult<Dependent>> DeleteAllBasicInfoAttachments(int employeeId)
        {
            var attachments = _context.AllAttachments.Where(attachment => attachment.EmployeeId == employeeId);
            if (attachments == null)
            {
                return NotFound();
            }

            foreach (BasicInfoAttachments current in attachments)
            {
                _context.AllAttachments.Remove(current);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        // DELETE: api/BasicInfoAttachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasicInfoAttachments>> DeleteBasicInfoAttachments(int id)
        {
            var basicInfoAttachments = await _context.AllAttachments.FindAsync(id);
            if (basicInfoAttachments == null)
            {
                return NotFound();
            }

            _context.AllAttachments.Remove(basicInfoAttachments);
            await _context.SaveChangesAsync();

            return basicInfoAttachments;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool BasicInfoAttachmentsExists(int id)
        {
            return _context.AllAttachments.Any(e => e.Id == id);
        }
    }
}
