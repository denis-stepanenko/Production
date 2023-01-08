using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionCardOperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public PermissionCardOperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int permissionCardId)
        {
            var items = await _context.PermissionCardOperations
                .Where(x => x.PermissionCardId == permissionCardId)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PermissionCardOperation item)
        {
            _context.PermissionCardOperations.Add(item);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new {id = item.Id}, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PermissionCardOperation item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.PermissionCardOperations.FindAsync(id);

            if (item is null)
                return NotFound();

            _context.PermissionCardOperations.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
