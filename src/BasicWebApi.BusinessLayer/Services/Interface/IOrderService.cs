using BasicWebApi.Shared.Models;
using BasicWebApi.Shared.Models.Req.Order;

namespace BasicWebApi.BusinessLayer.Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> SaveAsync(SaveOrder order);
    }
}