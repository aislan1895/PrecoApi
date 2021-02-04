using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using PrecoApi.Domain.ExternalApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecoApi.Service.Interface
{
    public interface IProductPriceService
    {
        BestPriceReturn GetBestPrice(List<ReturnPrice> priceList);
        Task<ReturnPrice> GetPriceAzulAsync(string productId, string storeId);
        Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj);
        ReturnPrice GetPriceOuro(ReturnPrice returnPriceBase, long storeId, MedalCode medalCode);
        ReturnPrice GetPriceSenior(ReturnPrice baseReturnPrice, long storeId, MedalCode medalCode);
        ReturnPrice GetPriceEncarte(ReturnPrice returnPrice, long storeId, MedalCode codeMedal);
    }
}
