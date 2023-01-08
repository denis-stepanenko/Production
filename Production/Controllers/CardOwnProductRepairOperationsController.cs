using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardOwnProductRepairOperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardOwnProductRepairOperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int cardOwnProductId)
        {
            var items = await _context.CardOwnProductRepairOperations
                .Include(x => x.Executor)
                .Where(x => x.CardOwnProductId == cardOwnProductId)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.CardOwnProductRepairOperations
                .Include(x => x.Executor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CardOwnProductRepairOperation item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CardOwnProductRepairOperation item)
        {
            _context.CardOwnProductRepairOperations.Add(item);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.CardOwnProductRepairOperations.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.CardOwnProductRepairOperations.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
