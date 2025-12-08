using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Stackbuld_API.Api
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _svc;
        public OrdersController(IOrderService svc) { _svc = svc; }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest req)
        {
            var res = await _svc.PlaceOrderAsync(req);
            if (!res.Success) return BadRequest(new { res.Message });
            return Ok(new { res.OrderId, res.Message });
        }
    }

}