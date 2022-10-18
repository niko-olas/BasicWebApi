using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicWebApi.BusinessLayer.Services.Common;
using BasicWebApi.BusinessLayer.Services.Interface;
using BasicWebApi.DataAccessLayer;
using BasicWebApi.Shared.Enums;
using BasicWebApi.Shared.Models;
using BasicWebApi.Shared.Models.Req.Order;
using Microsoft.EntityFrameworkCore;
using OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities = BasicWebApi.DataAccessLayer.Entity;
using Models = BasicWebApi.Shared.Models;

namespace BasicWebApi.BusinessLayer.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Result<IEnumerable<Order>>> GetOrders()
        {
            var query = base.Context.GetData<Entities.Orders>();
           
            var orders = await query.OrderByDescending(o => o.CreationDate)
                .ProjectTo<Models.Order>(base.Mapper.ConfigurationProvider)
                .ToListAsync();

            //var people = mapper.Map<IEnumerable<Order>>(dbOrder);

            return Result.Fail(FailureReasons.ClientError, "Unable to create a person with same first name and surname within 1 minute");
        }

        public async Task<Result<Order>> SaveAsync(SaveOrder order)
        {
            var dbOrder = order.Id != null ? await Context.GetData<Entities.Orders>(trackingChanges: true)
           .FirstOrDefaultAsync(o => o.Id == order.Id) : null;

            if (dbOrder == null)
            {
                dbOrder = Mapper.Map<Entities.Orders>(order);
                dbOrder.CreationDate = DateTime.UtcNow;
                dbOrder.Status = OrderStatus.New;

                Context.Insert(dbOrder);
            }
            else
            {
                Mapper.Map(order, dbOrder);
                dbOrder.LastModifiedDate = DateTime.UtcNow;
            }

            await Context.SaveAsync();

            var savedOrder = Mapper.Map<Order>(dbOrder);
            return savedOrder;
        }
    }
}
