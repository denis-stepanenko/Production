using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionCardsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public PermissionCardsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page)
        {
            var items = await _context.PermissionCards.PaginateAsync(page);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.PermissionCards
                .Include(x => x.Products)
                .Include(x => x.PurchasedProducts)
                .Include(x => x.Materials)
                .Include(x => x.Operations)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PermissionCard item)
        {
            await _context.PermissionCards.AddAsync(item);

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PermissionCard item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.PermissionCards.FindAsync(id);

            if (item is null)
                return NotFound();

            return NoContent();
        }
    }
}
