using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardPurchasedProductsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardPurchasedProductsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int cardId)
        {
            return Ok(await _context.CardPurchasedProducts.Where(x => x.CardId == cardId).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CardPurchasedProduct item)
        {
            _context.CardPurchasedProducts.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CardPurchasedProduct item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<CardPurchasedProduct> updates)
        {
            var item = await _context.CardPurchasedProducts.FindAsync(id);

            if (item is null)
                return NotFound();

            updates.ApplyTo(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.CardPurchasedProducts.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.CardPurchasedProducts.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
