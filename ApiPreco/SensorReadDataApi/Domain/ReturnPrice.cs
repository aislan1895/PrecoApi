using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain
{
    public class ReturnPrice
    {
        public long ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MaximumPrice { get; set; }
        public decimal PercentageDiscount { get; set; }
        public string DiscountType { get; set; }
    }
}
