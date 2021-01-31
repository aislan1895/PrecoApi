using PrecoApi.Domain;
using PrecoApi.Domain.ExternalApi;
using PrecoApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Controller
{
    public class GetPriceController
    {
        //Ficará os métodos responsaveis por pegar cada tipo de preço

        private readonly IProductPriceService _productPriceService;


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

        public PriceReturn GetPriceSegmentado(RequisitionData requisitionData)
        {
            var discountSegmentationForProducts = new List<DiscountSegmentation>();

            foreach (var product in requisitionData.Products)
            {
                DiscountSegmentation discountSegmentation = _productPriceService.GetDiscountSegmentation(Convert.ToInt32(product.Id), Convert.ToInt32(requisitionData.StoreId), 2);
                discountSegmentationForProducts.Add(discountSegmentation);
            }
            return new PriceReturn();
        }

        public static PriceReturn GetPriceEncarte()
        {
            return new PriceReturn();
        }

        public static BestPriceReturn GetBestPrice(List<PriceReturn> priceList)
        {
            PriceReturn priceReturn = priceList.OrderBy(x => x.SalePrice).ToList()[0];

            return new BestPriceReturn
            {
                Bestprice = Math.Round(priceReturn.SalePrice),
                DiscountType = priceReturn.DiscountType,
                FromPrice = Math.Round(priceReturn.MaximumPrice),
                ProductId = priceReturn.ProductId,
                DiscountPercentage = Math.Round(GestDiscount(priceReturn),2)
            };
        }

        private static decimal GestDiscount(PriceReturn priceReturn)
        {
            return ((priceReturn.MaximumPrice - priceReturn.SalePrice) / priceReturn.MaximumPrice) * 100;
        }
    }
}
