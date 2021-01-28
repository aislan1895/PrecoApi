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

        public static Price GetPriceOuro()
        {
            return new Price();
        }

        public static Price GetPriceAzul()
        {
            return new Price();
        }

        public static Price GetPriceSenior()
        {
            return new Price();
        }

        public static Price GetPriceDSM()
        {
            return new Price();
        }

        public static Price GetPriceSegmentado()
        {
            return new Price();
        }

        public static Price GetPriceEncarte()
        {
            return new Price();
        }

        public static BestPriceReturn GetBestPrice(List<Price> priceList)
        {
            return new BestPriceReturn();
        }
    }    
}
