using PrecoApi.Domain;
using System.Collections.Generic;

namespace PrecoApi.Service.Interface
{
    public interface IProductPriceService
    {
        MedalDiscount GetMedalDiscount(long productCode, long storeId, long medalCode);
        BestPriceReturn GetBestPrice(List<PriceReturn> priceList);
    }
}
