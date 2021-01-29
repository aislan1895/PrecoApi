using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecoApi.Domain;

namespace PrecoApi.Repository
{
    public interface IPrecoRepository
    {
        public IEnumerable<PriceReturn> ListAll();

        public DiscountSegmentation GetDiscountSegmentation(long productCode, long filial, long medalCode);

    }
}
