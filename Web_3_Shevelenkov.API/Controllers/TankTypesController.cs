using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.API.Data;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TankTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TankTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TankType>>> GetTankTypes()
        {
          if (_context.TankTypes == null)
          {
              return NotFound();
          }
            return await _context.TankTypes.ToListAsync();
        }

        // GET: api/TankTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TankType>> GetTankType(int id)
        {
          if (_context.TankTypes == null)
          {
              return NotFound();
          }
            var tankType = await _context.TankTypes.FindAsync(id);

            if (tankType == null)
            {
                return NotFound();
            }

            return tankType;
        }

        // PUT: api/TankTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTankType(int id, TankType tankType)
        {
            if (id != tankType.Id)
            {
                return BadRequest();
            }

            _context.Entry(tankType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TankTypeExists(id))
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

        // POST: api/TankTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TankType>> PostTankType(TankType tankType)
        {
          if (_context.TankTypes == null)
          {
              return Problem("Entity set 'AppDbContext.TankTypes'  is null.");
          }
            _context.TankTypes.Add(tankType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTankType", new { id = tankType.Id }, tankType);
        }

        // DELETE: api/TankTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTankType(int id)
        {
            if (_context.TankTypes == null)
            {
                return NotFound();
            }
            var tankType = await _context.TankTypes.FindAsync(id);
            if (tankType == null)
            {
                return NotFound();
            }

            _context.TankTypes.Remove(tankType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TankTypeExists(int id)
        {
            return (_context.TankTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
