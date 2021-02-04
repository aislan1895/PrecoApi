using PrecoApi.Domain;
using PrecoApi.Domain.Enum;

namespace PrecoApi.Repository
{
    public interface IPrecoRepository
    {
        MedalDiscount GetMedalDiscount(long productCode, long filial, MedalCode medalCode);
        PriceEncarte GetPriceEncarte(long productId, long storeId, MedalCode medalCode);
    }
}
