using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using PrecoApi.Domain.ExternalApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecoApi.Service.Interface
{
    public interface IProductPriceService
    {
        List<BestPriceReturn> GetPrice(RequisitionData request);
    }
}