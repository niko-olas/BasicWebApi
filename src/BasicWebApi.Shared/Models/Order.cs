using BasicWebApi.Shared.Enums;
using BasicWebApi.Shared.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Shared.Models
{
    public class Order : BaseObject
    {
        public DateTime CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public double TotalPrice { get; set; }

        public OrderStatus Status { get; set; }
    }
}
