using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateOperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public TemplateOperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int id)
        {
            var items = await _context.TemplateOperations.PaginateAsync(id);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.TemplateOperations.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TemplateOperation item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TemplateOperation item)
        {
            _context.TemplateOperations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.TemplateOperations.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }
    }
}
