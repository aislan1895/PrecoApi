using PrecoApi.Controller;
using PrecoApi.Domain;
using PrecoApi.Domain.ExternalApi;
using PrecoApi.Repository;
using PrecoApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace PrecoApi.Service
{
    public class ProductPriceService : IProductPriceService
    {
        HttpClient httpClient = new HttpClient();

        private readonly IPrecoRepository _precoRepository;

        public static PriceReturn GetPriceOuro()
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

        public static PriceReturn GetPriceEncarte()
        {
            return new PriceReturn();
        }
        public async Task<Price> GetPriceAzulAsync(string productId, string storeId)
        {
            PriceModel priceModel = await new RequestServices<PriceModel>(httpClient).SendResquest($"https://dev.apipmenos.com/price/v1/" + productId + "?subsidiaryId=" + storeId,

            return priceModel.price;
        }

        public async Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj)
        {
            CustomerScoreModel customerScoreModel = await new RequestServices<CustomerScoreModel>(httpClient).SendResquest($"https://dev.apipmenos.com/customer/v1/score/" + cpfCnpj,
                                                                                                                                               "4IBFLIlhDo4Uo9wXMGLd22JxPIax3DZwaNMSnK5w");
            return customerScoreModel.customerScore;
        }

        public MedalDiscount GetMedalDiscount(long productId, long storeId, long medalCode)
        {
            MedalDiscount medalDiscount = _precoRepository.GetMedalDiscount(productId, storeId, medalCode);
            
            return medalDiscount;
        }

        public BestPriceReturn GetBestPrice(List<PriceReturn> priceList)
        {
            PriceReturn priceReturn = priceList.OrderBy(x => x.SalePrice).ToList()[0];

            return new BestPriceReturn
            {
                Bestprice = Math.Round(priceReturn.SalePrice),
                DiscountType = priceReturn.DiscountType,
                FromPrice = Math.Round(priceReturn.MaximumPrice),
                ProductId = priceReturn.ProductId,
                DiscountPercentage = Math.Round(GetDiscount(priceReturn), 2)
            };
        }

        private static decimal GetDiscount(PriceReturn priceReturn)
        {
            return ((priceReturn.MaximumPrice - priceReturn.SalePrice) / priceReturn.MaximumPrice) * 100;
        }
    }
}