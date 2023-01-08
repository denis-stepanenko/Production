using Microsoft.AspNetCore.Mvc;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRelationsController : ControllerBase
    {
        private readonly ProductionContext _context;

        public ProductRelationsController(ProductionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить информацию о том, какие продукты входят в изделие, их количестве, маршруте
        /// </summary>
        /// <param name="code">Код изделия</param>
        /// <param name="count">Количество изделия</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string code, [FromQuery]int count)
        {
            var items = await _context.GetProductRelationsAsync(code, count);

            return Ok(items);
        }
    }
}
