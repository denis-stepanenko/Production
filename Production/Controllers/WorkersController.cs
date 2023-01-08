using Microsoft.AspNetCore.Mvc;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly ProductionContext _context;

        public WorkersController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page)
        {
            return Ok(await _context.Workers.PaginateAsync(page));
        }
    }
}
