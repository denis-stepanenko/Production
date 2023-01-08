using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutorsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public ExecutorsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            return Ok(await _context.Executors.PaginateAsync(page));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Executors.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Executor item)
        {
            _context.Executors.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Executor item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<Executor> updates)
        {
            var item = await _context.Executors.FindAsync(id);

            if (item is null)
                return NotFound();

            updates.ApplyTo(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Executors.FindAsync(id);

            if(item is null)
                return NotFound();

            _context.Executors.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
