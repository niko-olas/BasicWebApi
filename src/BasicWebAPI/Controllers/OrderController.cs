using Microsoft.AspNetCore.Mvc;
using BasicWebApi.BusinessLayer.Services.Interface;
using BasicWebApi.Shared.Models.Req.Order;
using OperationResults.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using BasicWebApi.Shared.Models.Res.Order;

namespace BasicWebAPI.Controllers
{
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

       
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        /// <summary>
        /// Ottine una lista di ordini
        /// </summary>
        /// <response code="200">List orders</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList()
        {
            var orders = await orderService.GetOrders();
            return HttpContext.CreateResponse(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<IActionResult> Save(SaveOrder order)
        {
            var savedOrder = await orderService.SaveAsync(order);
            return HttpContext.CreateResponse(savedOrder);
        }


    }
}
