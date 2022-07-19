using Microsoft.AspNetCore.Mvc;
using BasicWebApi.BusinessLayer.Services.Interface;
using BasicWebApi.Shared.Models.Req.Order;

namespace BasicWebAPI.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var orders = await orderService.GetOrders();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaveOrder order)
        {
            var savedOrder = await orderService.SaveAsync(order);
            return Ok(savedOrder);
        }


    }
}
