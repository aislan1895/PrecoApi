using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using PrecoApi.Domain.ExternalApi;
using PrecoApi.Repository;
using PrecoApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrecoApi.Service
{
    public class ProductPriceService : IProductPriceService
    {

        private readonly IPrecoRepository _precoRepository;
        private readonly HttpClient httpClient = new HttpClient();

        public ProductPriceService(IPrecoRepository precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public ReturnPrice GetPriceOuro(ReturnPrice baseReturnPrice, long storeId, MedalCode medalCode)
        {
            MedalDiscount medalDiscount = GetMedalDiscount(baseReturnPrice.ProductId, storeId, medalCode);

            ReturnPrice returnPrice = new ReturnPrice
            {
                DiscountType = DiscountType.Ouro,
                MaximumPrice = baseReturnPrice.SalePrice,
                PercentageDiscount = medalDiscount.PercentualDesconto,
                ProductId = baseReturnPrice.ProductId,
                SalePrice = baseReturnPrice.SalePrice - Decimal.Multiply(baseReturnPrice.SalePrice, medalDiscount.PercentualDesconto / 100)
            };

            return returnPrice;
        }

        public ReturnPrice GetPriceSenior(ReturnPrice baseReturnPrice, long storeId, MedalCode medalCode)
        {
            MedalDiscount medalDiscount = GetMedalDiscount(baseReturnPrice.ProductId, storeId, medalCode);

            ReturnPrice returnPrice = new ReturnPrice
            {
                DiscountType = DiscountType.Senior,
                MaximumPrice = baseReturnPrice.MaximumPrice,
                PercentageDiscount = medalDiscount.PercentualDesconto,
                ProductId = baseReturnPrice.ProductId,
                SalePrice = baseReturnPrice.MaximumPrice - Decimal.Multiply(baseReturnPrice.SalePrice, medalDiscount.PercentualDesconto / 100)
            };

            return returnPrice;
        }

        public static ReturnPrice GetPriceDSM()
        {
            return new ReturnPrice();
        }

        public ReturnPrice GetPriceEncarte(ReturnPrice returnPrice, long storeId, MedalCode codeMedal)
        {
            PriceEncarte priceEncarte = _precoRepository.GetPriceEncarte(returnPrice.ProductId, storeId, codeMedal);

            return new ReturnPrice
            {
                ProductId = returnPrice.ProductId,
                SalePrice = priceEncarte.Price,
                MaximumPrice = returnPrice.SalePrice,
                DiscountType = DiscountType.Encarte,
                PercentageDiscount = Math.Round(((returnPrice.SalePrice - priceEncarte.Price) / returnPrice.SalePrice) * 100,2)
            };
        }

        public async Task<ReturnPrice> GetPriceAzulAsync(string productId, string storeId)
        {
            PriceModel priceModel = await new RequestService<PriceModel>(httpClient).SendResquest($"https://dev.apipmenos.com/price/v1/" + productId + "?subsidiaryId=" + storeId, "vhubPbOuqb7X5ZEuflnJN1c3GlR03K2x4KzAt6d1");

            ReturnPrice priceReturn = new ReturnPrice
            {
                DiscountType = DiscountType.Azul,
                MaximumPrice = Decimal.Parse(priceModel.price.maxPrice),
                PercentageDiscount = 0,
                ProductId = int.Parse(productId),
                SalePrice = Decimal.Parse(priceModel.price.everBluePrice)
            };

            return priceReturn;
        }

        public async Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj)
        {
            CustomerScoreModel customerScoreModel = await new RequestService<CustomerScoreModel>(httpClient).SendResquest($"https://dev.apipmenos.com/customer/v1/score/" + cpfCnpj, "4IBFLIlhDo4Uo9wXMGLd22JxPIax3DZwaNMSnK5w");
            return customerScoreModel.customerScore;
        }

        public BestPriceReturn GetBestPrice(List<ReturnPrice> priceList)
        {
            ReturnPrice priceReturn = priceList.OrderBy(x => x.SalePrice).ToList()[0];

            return new BestPriceReturn
            {
                Bestprice = Math.Round(priceReturn.SalePrice,2),
                DiscountType = priceReturn.DiscountType,
                FromPrice = Math.Round(priceReturn.MaximumPrice,2),
                ProductId = priceReturn.ProductId,
                DiscountPercentage = priceReturn.PercentageDiscount
            };
        }

        private MedalDiscount GetMedalDiscount(long productId, long storeId, MedalCode medalCode)
        {
            MedalDiscount medalDiscount = _precoRepository.GetMedalDiscount(productId, storeId, medalCode);

            return medalDiscount;
        }
    }
}