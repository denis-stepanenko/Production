using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnlockedPeriodsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public UnlockedPeriodsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.UnlockedPeriods.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UnlockedPeriod item)
        {
            await _context.UnlockedPeriods.AddAsync(item);

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UnlockedPeriod item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<UnlockedPeriod> updates)
        {
            var item = await _context.UnlockedPeriods.FindAsync(id);

            if (item is null)
                return NotFound();

            updates.ApplyTo(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.UnlockedPeriods.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.UnlockedPeriods.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
