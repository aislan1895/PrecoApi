using PrecoApi.Domain;

namespace PrecoApi.Service.Interface
{
    public interface IProductPriceService
    {
        DiscountSegmentation GetDiscountSegmentation(long productCode, long filial, long medalCode);
    }
}
