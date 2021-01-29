using PrecoApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Controller
{
    public static class GetPriceController
    {
        //Ficará os métodos responsaveis por pegar cada tipo de preço

        public static PriceReturn GetPriceOuro()
        {
            return new PriceReturn();
        }

        public static PriceReturn GetPriceAzul()
        {
            return new PriceReturn();
        }

        public static PriceReturn GetPriceSenior()
        {
            return new PriceReturn();
        }

        public static PriceReturn GetPriceDSM()
        {
            return new PriceReturn();
        }

        public static PriceReturn GetPriceSegmentado()
        {
            return new PriceReturn();
        }

        public static PriceReturn GetPriceEncarte()
        {
            return new PriceReturn();
        }

        public static BestPriceReturn GetBestPrice(List<PriceReturn> priceList)
        {   
            return new BestPriceReturn();
        }
    }    
}
