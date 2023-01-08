using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ProductionContext _context;

        public TemplatesController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page)
        {
            var items = await _context.Templates.PaginateAsync(page);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Templates
                .Include(x => x.TemplateOperations)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Template item)
        {
            await _context.Templates.AddAsync(item);

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Template item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.Templates.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.Templates.Remove(item);

            await _context.SaveChangesAsync();

            return Ok(item);
        }
    }
}
