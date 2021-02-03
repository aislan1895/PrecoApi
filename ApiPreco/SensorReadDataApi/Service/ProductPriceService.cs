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
using PrecoApi.Domain.Enum;

namespace PrecoApi.Service
{
    public class ProductPriceService : IProductPriceService
    {
        HttpClient httpClient = new HttpClient();

        private readonly IPrecoRepository _precoRepository;

        public ProductPriceService(IPrecoRepository precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public ReturnPrice GetPriceOuroAndSenior(ReturnPrice baseReturnPrice, long storeId, CodeMedal medalCode)
        {
            MedalDiscount medalDiscount = GetMedalDiscount(baseReturnPrice.ProductId, storeId, medalCode);

            ReturnPrice returnPrice = new ReturnPrice
            {
                DiscountType = medalCode.ToString(),
                MaximumPrice = baseReturnPrice.MaximumPrice,
                PercentageDiscount = medalDiscount.PercentualDesconto,
                ProductId = baseReturnPrice.ProductId,
                SalePrice = baseReturnPrice.SalePrice - Decimal.Multiply(baseReturnPrice.SalePrice, medalDiscount.PercentualDesconto / 100)
            };
            return returnPrice;
        }

        public static ReturnPrice GetPriceDSM()
        {
            return new ReturnPrice();
        }

        public static ReturnPrice GetPriceEncarte()
        {
            return new ReturnPrice();
        }
        public async Task<ReturnPrice> GetPriceAzulAsync(string productId, string storeId)
        {
            PriceModel priceModel = await new RequestServices<PriceModel>(httpClient).SendResquest($"https://dev.apipmenos.com/price/v1/" + productId + "?subsidiaryId=" + storeId, "vhubPbOuqb7X5ZEuflnJN1c3GlR03K2x4KzAt6d1");

            ReturnPrice priceReturn = new ReturnPrice
            {
                DiscountType = CodeMedal.Azul.ToString(),
                MaximumPrice = Decimal.Parse(priceModel.price.maxPrice),
                PercentageDiscount = 0,
                ProductId = int.Parse(productId),
                SalePrice = Decimal.Parse(priceModel.price.everBluePrice)
            };

            return priceReturn;
        }

        public async Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj)
        {
            CustomerScoreModel customerScoreModel = await new RequestServices<CustomerScoreModel>(httpClient).SendResquest($"https://dev.apipmenos.com/customer/v1/score/" + cpfCnpj, "4IBFLIlhDo4Uo9wXMGLd22JxPIax3DZwaNMSnK5w");
            return customerScoreModel.customerScore;
        }

        private MedalDiscount GetMedalDiscount(long productId, long storeId, CodeMedal medalCode)
        {
            MedalDiscount medalDiscount = _precoRepository.GetMedalDiscount(productId, storeId, medalCode);

            return medalDiscount;
        }

        public BestPriceReturn GetBestPrice(List<ReturnPrice> priceList)
        {
            ReturnPrice priceReturn = priceList.OrderBy(x => x.SalePrice).ToList()[0];

            return new BestPriceReturn
            {
                Bestprice = priceReturn.SalePrice,
                DiscountType = priceReturn.DiscountType,
                FromPrice = priceReturn.MaximumPrice,
                ProductId = priceReturn.ProductId,
                DiscountPercentage = priceReturn.PercentageDiscount
            };
        }

        //private static decimal GetDiscount(ReturnPrice priceReturn)
        //{
        //    return ((priceReturn.MaximumPrice - priceReturn.SalePrice) / priceReturn.MaximumPrice) * 100;
        //}
    }
}