using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System.Data;
using Employee_Management_System.Models;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EployeesController : ControllerBase
    {
        private readonly Assignment _context;

        public EployeesController(Assignment context)
        {
            _context = context;
        }

        // GET: api/Eployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eployee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Eployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Eployee>> GetEployee(int id)
        {
            var eployee = await _context.Employees.FindAsync(id);

            if (eployee == null)
            {
                return NotFound();
            }

            return eployee;
        }

        // PUT: api/Eployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEployee(int id, Eployee eployee)
        {
            if (id != eployee.Id)
            {
                return BadRequest();
            }

            _context.Entry(eployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EployeeExists(id))
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

        // POST: api/Eployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Eployee>> PostEployee(Eployee eployee)
        {
            _context.Employees.Add(eployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEployee", new { id = eployee.Id }, eployee);
        }

        // DELETE: api/Eployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEployee(int id)
        {
            var eployee = await _context.Employees.FindAsync(id);
            if (eployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(eployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
