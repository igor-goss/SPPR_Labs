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
    public class TanksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TanksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tanks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tank>>> GetTanks()
        {
          if (_context.Tanks == null)
          {
              return NotFound();
          }
            return await _context.Tanks.ToListAsync();
        }

        // GET: api/Tanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tank>> GetTank(int id)
        {
          if (_context.Tanks == null)
          {
              return NotFound();
          }
            var tank = await _context.Tanks.FindAsync(id);

            if (tank == null)
            {
                return NotFound();
            }

            return tank;
        }

        // PUT: api/Tanks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTank(int id, Tank tank)
        {
            if (id != tank.Id)
            {
                return BadRequest();
            }

            _context.Entry(tank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TankExists(id))
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

        // POST: api/Tanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tank>> PostTank(Tank tank)
        {
          if (_context.Tanks == null)
          {
              return Problem("Entity set 'AppDbContext.Tanks'  is null.");
          }
            _context.Tanks.Add(tank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTank", new { id = tank.Id }, tank);
        }

        // DELETE: api/Tanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTank(int id)
        {
            if (_context.Tanks == null)
            {
                return NotFound();
            }
            var tank = await _context.Tanks.FindAsync(id);
            if (tank == null)
            {
                return NotFound();
            }

            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TankExists(int id)
        {
            return (_context.Tanks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
