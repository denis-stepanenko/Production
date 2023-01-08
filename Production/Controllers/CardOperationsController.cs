using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardOperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardOperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int cardId)
        {
            return Ok(await _context.CardOperations.Where(x => x.CardId == cardId).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CardOperation item)
        {
            _context.CardOperations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CardOperation item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<CardOperation> updates)
        {
            var item = await _context.CardOperations.FindAsync(id);

            if(item is null)
                return NotFound();

            updates.ApplyTo(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.CardOperations
                .Include(x => x.Card)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(item is null)
                return NotFound();
             
            if ((item.Card.IsDepartment4Confirmed && item.Department == 4) ||
                (item.Card.IsDepartment5Confirmed && item.Department == 5) ||
                (item.Card.IsDepartment6Confirmed && item.Department == 6) ||
                (item.Card.IsDepartment13Confirmed && item.Department == 13) ||
                (item.Card.IsDepartment17Confirmed && item.Department == 17) ||
                (item.Card.IsDepartment80Confirmed && item.Department == 80) ||
                (item.Card.IsDepartment82Confirmed && item.Department == 82))
            {
                return BadRequest("OOIOT confirmed the department of this operation");
            }

            if (item.Date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
            {
                if (!_context.UnlockedPeriods.Any(x => x.Year == item.Date.Value.Year && x.Month == item.Date.Value.Month && x.CardId == item.CardId))
                {
                    return BadRequest("You cannot work with a closed period");
                }
            }

            _context.CardOperations.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
