using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = BasicWebApi.Shared.Models.Req.Order;

namespace BasicWebApi.BusinessLayer.Validation
{
    public class SaveOrderRequest : AbstractValidator<Models.SaveOrder>
    {
        public SaveOrderRequest()
        {
            RuleFor(o => o.TotalPrice).GreaterThan(0)
            .WithMessage("Unable to create an order with 0 or negative total price");
        }
    }
}
