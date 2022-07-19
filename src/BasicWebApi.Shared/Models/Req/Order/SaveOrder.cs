using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Shared.Models.Req.Order
{
    public class SaveOrder
    {
        public Guid? Id { get; set; }

        public double TotalPrice { get; set; }
    }
}
