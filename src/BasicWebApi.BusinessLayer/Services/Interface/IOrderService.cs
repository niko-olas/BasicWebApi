using BasicWebApi.Shared.Models;
using BasicWebApi.Shared.Models.Req.Order;
using OperationResults;

namespace BasicWebApi.BusinessLayer.Services.Interface
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetOrders();
        Task<Result<Order>> SaveAsync(SaveOrder order);
    }
}