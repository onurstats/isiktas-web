using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class OClassesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OClassesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/OClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OClass>>> GetOClass()
        {
            return await _context.OClass.ToListAsync();
        }

        // GET: api/OClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OClass>> GetOClass(Guid id)
        {
            var oClass = await _context.OClass.FindAsync(id);

            if (oClass == null)
            {
                return NotFound();
            }

            return oClass;
        }

        // PUT: api/OClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOClass(Guid id, OClass oClass)
        {
            if (id != oClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(oClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OClassExists(id))
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

        // POST: api/OClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OClass>> PostOClass(OClass oClass)
        {
            _context.OClass.Add(oClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOClass", new { id = oClass.Id }, oClass);
        }

        // DELETE: api/OClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OClass>> DeleteOClass(Guid id)
        {
            var oClass = await _context.OClass.FindAsync(id);
            if (oClass == null)
            {
                return NotFound();
            }

            _context.OClass.Remove(oClass);
            await _context.SaveChangesAsync();

            return oClass;
        }

        private bool OClassExists(Guid id)
        {
            return _context.OClass.Any(e => e.Id == id);
        }
    }
}
