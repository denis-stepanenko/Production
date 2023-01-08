using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public MaterialsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            if (page == 0)
                return BadRequest();

            var result = await _context.Materials.Include(x => x.Unit).PaginateAsync(page);

            return Ok(result);
        }
    }
}
