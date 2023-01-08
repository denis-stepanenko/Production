using Microsoft.AspNetCore.Mvc;

namespace Production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ProductionContext _context;

        public OrdersController(ProductionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список заказов, в которые входит продукт
        /// </summary>
        /// <param name="productCode">Код продукта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string productCode)
        {
            var items = await _context.GetOrdersByProductAsync(productCode);

            return Ok(items);
        }
    }
}
