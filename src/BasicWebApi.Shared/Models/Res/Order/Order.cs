using BasicWebApi.Shared.Enums;
using BasicWebApi.Shared.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Shared.Models.Res.Order
{
    public class Order : BaseObject
    {

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }
    }
}
