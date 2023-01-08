using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsToCreateIn1SController : ControllerBase
    {
        private readonly ProductionContext _context;

        public RequestsToCreateIn1SController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page)
        {
            var items = await _context.RequestsToCreateStagesIn1S.PaginateAsync(page);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.RequestsToCreateStagesIn1S.FindAsync(id);

            if(item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RequestToCreateStagesIn1S item)
        {
            _context.RequestsToCreateStagesIn1S.Add(item);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RequestToCreateStagesIn1S item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        { 
            var item = await _context.RequestsToCreateStagesIn1S.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.RequestsToCreateStagesIn1S.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
