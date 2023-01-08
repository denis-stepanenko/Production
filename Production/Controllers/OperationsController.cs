using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public OperationsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            if (page == 0)
                return BadRequest();

            return Ok(await _context.Operations.PaginateAsync(page));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Operations.FindAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Operation item)
        {
            if (_context.Operations.Any(x => x.Code == item.Code))
            {
                return BadRequest("There is already an operation with this code");
            }

            _context.Operations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Operation item)
        {
            if (_context.Operations.Any(x => x.Code == item.Code && x.Id != item.Id))
            {
                return BadRequest("There is already an operation with this code");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<Operation> updates)
        {
            var item = await _context.Operations.FindAsync(id);

            if (item is null)
                return NotFound();

            updates.ApplyTo(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Operations.FindAsync(id);

            if (item is null) 
                return NotFound();

            _context.Operations.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
