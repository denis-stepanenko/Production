using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionCardPurchasedProductsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public PermissionCardPurchasedProductsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int permissionCardId)
        {
            var items = await _context.PermissionCardPurchasedProducts
                .Where(x => x.PermissionCardId == permissionCardId)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.PermissionCardPurchasedProducts.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PermissionCardPurchasedProduct item)
        {
            _context.PermissionCardPurchasedProducts.Add(item);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PermissionCardPurchasedProduct item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.PermissionCardPurchasedProducts.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.PermissionCardPurchasedProducts.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
