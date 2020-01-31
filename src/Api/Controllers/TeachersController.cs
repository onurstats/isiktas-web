using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TeachersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherShortList>>> GetTeachers()

        {

            return await _context.Teachers
                .Include(x => x.SubjectTeachers)
                .ThenInclude(y => y.Subject)
                .Select(x => new TeacherShortList
                {
                    Id = x.Id,
                    Fullname = x.FullName,
                    Subjects = x.SubjectTeachers
                                    .Select(t => t.Subject.Id ).ToArray()
                })
                .ToListAsync();

        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDetail>> GetTeacher(Guid id)
        {
            var teacher = await _context.Teachers
                .Include(x => x.SubjectTeachers)
                .ThenInclude(y => y.Subject)
                .Select(x=> new TeacherDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Tck= x.Tck,
                    Tel=x.Tel,
                    Subjects = x.SubjectTeachers
                                    .Select(t => t.Subject.Id).ToArray()
                })
                .SingleOrDefaultAsync(i => i.Id == id); 

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(Guid id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            var old = _context.SubjectTeachers.Where(x => x.TeacherId == id);

            _context.SubjectTeachers.RemoveRange(old);
            _context.SubjectTeachers.AddRange(teacher.SubjectTeachers);

            _context.Entry(teacher).State = EntityState.Modified;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {

            _context.Teachers.Add(teacher);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        private bool TeacherExists(Guid id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
