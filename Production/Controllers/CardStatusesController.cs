using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class CardStatusesController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardStatusesController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.CardStatuses.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.CardStatuses.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CardStatus item)
        {
            _context.CardStatuses.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CardStatus item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<CardStatus> updates)
        {
            var item = await _context.CardStatuses.FindAsync(id);

            if(item is null)
                return NotFound();

            updates.ApplyTo(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CardStatuses.FindAsync(id);

            if(item is null) 
                return NotFound();

            _context.CardStatuses.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
