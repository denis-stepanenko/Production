using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public CardsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            var items = await _context.Cards
                .Include(x => x.CardStatus)
                .Include(x => x.RepairType)
                .PaginateAsync(page);

            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Cards
                .Include(x => x.CardStatus)
                .Include(x => x.RepairType)
                .Include(x => x.Operations)
                .Include(x => x.Materials)
                .Include(x => x.OwnProducts)
                .Include(x => x.PurchasedProducts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return NotFound();

            LoadChildrenRecursively(item);

            return Ok(item);
        }

        void LoadChildrenRecursively(Card card)
        {
            _context.Entry(card).Collection(x => x.Children).Load();

            foreach (var child in card.Children)
            {
                LoadChildrenRecursively(child);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Card item)
        {
            await _context.Cards.AddAsync(item);

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        /// <response code="400">Если нарушены правила бизнес-логики</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Card item)
        {
            if (item.Id == item.ParentId)
            {
                return BadRequest("Card cannot be a parent to itself");
            }

            if (_context.Cards.Any(x => x.Number == item.Number && x.Id != item.Id))
            {
                return BadRequest("There is already a card with this number");
            }

            if (_context.Cards.Any(x => x.ActNumber == item.ActNumber && x.Id != item.Id))
            {
                return BadRequest("There is already a card with this act number");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<Card> updates)
        {
            var item = await _context.Cards.FindAsync(id);

            if (item is null)
                return NotFound();

            updates.ApplyTo(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Cards.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.Cards.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
