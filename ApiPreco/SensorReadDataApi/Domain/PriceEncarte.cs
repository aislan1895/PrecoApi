using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain
{
    public class PriceEncarte
    {
        public long ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
