using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardOwnProductOperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardOwnProductOperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int cardOwnProductId)
        {
            var items = await _context.CardOwnProductsOperations.Where(x => x.CardOwnProductId == cardOwnProductId)
                .Include(x => x.Executor)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CardOwnProductOperation item)
        {
            _context.CardOwnProductsOperations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }


        [HttpPut]
        public async Task<IActionResult> Update(CardOwnProductOperation item)
        {
            if (item.ConfirmUserId != null)
            {
                return BadRequest("This operation is confirmed by OTK");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.CardOwnProductsOperations.FindAsync(id);

            if (item is null)
                return NotFound();

            if (item.ConfirmUserId != null)
            {
                return BadRequest("This operation is confirmed by OTK");
            }

            _context.CardOwnProductsOperations.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
