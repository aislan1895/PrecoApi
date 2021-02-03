using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain
{
    public class BestPriceReturn
    {
        public long ProductId { get; set; }

        public decimal FromPrice { get; set; }

        public decimal Bestprice { get; set; }

        public decimal DiscountPercentage { get; set; }

        public string DiscountType { get; set; }
    }
}
