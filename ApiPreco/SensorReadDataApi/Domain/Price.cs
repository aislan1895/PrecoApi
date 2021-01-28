using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain
{
    public class Price
    {
        public long ProductId { get; set; }
        public float SalePrice { get; set; }
        public decimal MaximumPrice { get; set; }
        public decimal PercentageDiscount { get; set; }
        public string DiscountType { get; set; }
    }
}
