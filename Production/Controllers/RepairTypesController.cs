using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairTypesController : ControllerBase
    {
        private readonly ProductionContext _context;

        public RepairTypesController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.RepairTypes.ToListAsync());
        }
    }
}
