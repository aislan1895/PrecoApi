using PrecoApi.Domain;
using PrecoApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrecoApi.Service
{
    public class ProductPriceService
    {
        private readonly IPrecoRepository _precoRepository;
        private DiscountSegmentation GetDiscountSegmentation(long productCode, long filial, long medalCode)
        {
            try
            {
                DiscountSegmentation discountSegmentation = _precoRepository.GetDiscountSegmentation(productCode, filial, medalCode);
                return discountSegmentation;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}