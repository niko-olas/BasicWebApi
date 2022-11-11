using BasicWebApi.Shared.Models.Req.Order;
using BasicWebApi.Shared.Models.Res.Order;
using OperationResults;

namespace BasicWebApi.BusinessLayer.Services.Interface
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetOrders();
        Task<Result<Order>> SaveAsync(SaveOrder order);
    }
}