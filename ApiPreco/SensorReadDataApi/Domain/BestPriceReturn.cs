using PrecoApi.Domain.Enum;

namespace PrecoApi.Domain
{
    public class BestPriceReturn
    {
        public long ProductId { get; set; }

        public decimal FromPrice { get; set; }

        public decimal Bestprice { get; set; }

        public decimal DiscountPercentage { get; set; }

        public DiscountType DiscountType { get; set; }
    }
}
