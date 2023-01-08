using Microsoft.AspNetCore.Mvc;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public ProductsController(ProductionContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if(item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page)
        {
            return Ok(await _context.Products.PaginateAsync(page));
        }
    }
}
