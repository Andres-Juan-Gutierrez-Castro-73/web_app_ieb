using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleccionsController : ControllerBase
    {
        private readonly WebApp2Context _context;

        public SeleccionsController(WebApp2Context context)
        {
            _context = context;
        }

        // GET: api/Seleccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seleccion>>> GetSeleccions()
        {
          if (_context.Seleccions == null)
          {
              return NotFound();
          }
            return await _context.Seleccions.ToListAsync();
        }

        // GET: api/Seleccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seleccion>> GetSeleccion(int id)
        {
          if (_context.Seleccions == null)
          {
              return NotFound();
          }
            var seleccion = await _context.Seleccions.FindAsync(id);

            if (seleccion == null)
            {
                return NotFound();
            }

            return seleccion;
        }

        // PUT: api/Seleccions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeleccion(int id, Seleccion seleccion)
        {
            if (id != seleccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(seleccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeleccionExists(id))
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

        // POST: api/Seleccions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seleccion>> PostSeleccion(Seleccion seleccion)
        {
          if (_context.Seleccions == null)
          {
              return Problem("Entity set 'WebApp2Context.Seleccions'  is null.");
          }
            _context.Seleccions.Add(seleccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeleccion", new { id = seleccion.Id }, seleccion);
        }

        // DELETE: api/Seleccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeleccion(int id)
        {
            if (_context.Seleccions == null)
            {
                return NotFound();
            }
            var seleccion = await _context.Seleccions.FindAsync(id);
            if (seleccion == null)
            {
                return NotFound();
            }

            _context.Seleccions.Remove(seleccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeleccionExists(int id)
        {
            return (_context.Seleccions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
