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
    public class CatalogoesController : ControllerBase
    {
        private readonly WebApp2Context _context;

        public CatalogoesController(WebApp2Context context)
        {
            _context = context;
        }

        // GET: api/Catalogoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetCatalogos()
        {
          if (_context.Catalogos == null)
          {
              return NotFound();
          }
            return await _context.Catalogos.ToListAsync();
        }

        // GET: api/Catalogoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalogo>> GetCatalogo(int id)
        {
          if (_context.Catalogos == null)
          {
              return NotFound();
          }
            var catalogo = await _context.Catalogos.FindAsync(id);

            if (catalogo == null)
            {
                return NotFound();
            }

            return catalogo;
        }

        // PUT: api/Catalogoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogo(int id, Catalogo catalogo)
        {
            if (id != catalogo.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalogo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(id))
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

        // POST: api/Catalogoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catalogo>> PostCatalogo(Catalogo catalogo)
        {
          if (_context.Catalogos == null)
          {
              return Problem("Entity set 'WebApp2Context.Catalogos'  is null.");
          }
            _context.Catalogos.Add(catalogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogo", new { id = catalogo.Id }, catalogo);
        }

        // DELETE: api/Catalogoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(int id)
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoExists(int id)
        {
            return (_context.Catalogos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
