using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using PrecoApi.Domain.ExternalApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecoApi.Service.Interface
{
    public interface IProductPriceService
    {
        //MedalDiscount GetMedalDiscount(long productCode, long storeId, CodeMedal medalCode);
        BestPriceReturn GetBestPrice(List<ReturnPrice> priceList);
        Task<ReturnPrice> GetPriceAzulAsync(string productId, string storeId);
        Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj);
        ReturnPrice GetPriceOuro(ReturnPrice returnPriceBase, long storeId, CodeMedal medalCode);
        ReturnPrice GetPriceSenior(ReturnPrice baseReturnPrice, long storeId, CodeMedal medalCode);
    }
}
