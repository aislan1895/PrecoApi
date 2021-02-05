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

        public List<BestPriceReturn> GetPrice(RequisitionData request)
        {
            List<BestPriceReturn> bestPriceReturnList = new List<BestPriceReturn>();

            CustomerScore customerScore = GetCustomerScoreAsync(request.CpfCnpjCustomer).Result;

            foreach (Product product in request.Products)
            {
                List<ReturnPrice> returnPriceList = new List<ReturnPrice>();

                ReturnPrice baseReturnprice = GetPriceAzulAsync(product.Id.ToString(), request.StoreId.ToString(), customerScore.Score != null).Result;

                ReturnPrice priceEncarte = GetPriceEncarte(baseReturnprice, customerScore, request.StoreId);

                if (priceEncarte.SalePrice > 0)
                    returnPriceList.Add(priceEncarte);

                if (customerScore.Score != null)
                {
                    if (customerScore.Score.Description == MedalCode.Ouro.ToString().ToUpper())
                        returnPriceList.Add(GetPriceOuro(baseReturnprice, request.StoreId, MedalCode.Ouro));
                    else if (customerScore.Score.Description == MedalCode.Senior.ToString().ToUpper())
                        returnPriceList.Add(GetPriceSenior(baseReturnprice, request.StoreId, MedalCode.Senior));
                    else
                        returnPriceList.Add(baseReturnprice);
                }
                else
                {
                    returnPriceList.Add(GetPriceNotRegistered(baseReturnprice));
                }

                bestPriceReturnList.Add(GetBestPrice(returnPriceList));
            }

            return bestPriceReturnList;
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
                SalePrice = baseReturnPrice.MaximumPrice - Decimal.Multiply(baseReturnPrice.MaximumPrice, medalDiscount.PercentualDesconto / 100)
            };

            return returnPrice;
        }

        public ReturnPrice GetPriceNotRegistered(ReturnPrice baseReturnPrice)
        {
            ReturnPrice returnPrice = new ReturnPrice
            {
                DiscountType = DiscountType.NãoCadastrado,
                MaximumPrice = baseReturnPrice.SalePrice,
                PercentageDiscount = 0,
                ProductId = baseReturnPrice.ProductId,
                SalePrice = baseReturnPrice.SalePrice
            };

            return returnPrice;
        }

        public static ReturnPrice GetPriceDSM()
        {
            return new ReturnPrice();
        }

        public async Task<ReturnPrice> GetPriceAzulAsync(string productId, string storeId, bool customerRegistered)
        {
            PriceModel priceModel = await new RequestService<PriceModel>(httpClient).SendResquest($"https://dev.apipmenos.com/price/v1/" + productId + "?subsidiaryId=" + storeId, "vhubPbOuqb7X5ZEuflnJN1c3GlR03K2x4KzAt6d1");
            Decimal SalePrice;

            if (customerRegistered)
                SalePrice = Decimal.Parse(priceModel.price.everBluePrice);
            else
                SalePrice = Decimal.Parse(priceModel.price.salePrice);


            ReturnPrice returnPrice = new ReturnPrice
            {
                DiscountType = DiscountType.Azul,
                MaximumPrice = Decimal.Parse(priceModel.price.salePrice),
                PercentageDiscount = Math.Round(((Decimal.Parse(priceModel.price.salePrice) - SalePrice) / Decimal.Parse(priceModel.price.salePrice)) * 100, 2),
                ProductId = int.Parse(productId),
                SalePrice = SalePrice
            };

            return returnPrice;
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
                Bestprice = Math.Round(priceReturn.SalePrice, 2),
                DiscountType = priceReturn.DiscountType,
                FromPrice = Math.Round(priceReturn.MaximumPrice, 2),
                ProductId = priceReturn.ProductId,
                DiscountPercentage = priceReturn.PercentageDiscount
            };
        }

        private ReturnPrice GetPriceEncarte(ReturnPrice returnPrice, CustomerScore customerScore, long storeId)
        {
            MedalCode medalCode;

            if (customerScore.Score == null)
                medalCode = MedalCode.NotRegistered;
            else
                medalCode = (MedalCode)int.Parse(customerScore.Score.Id);

            PriceEncarte priceEncarte = _precoRepository.GetPriceEncarte(returnPrice.ProductId, storeId, medalCode);

            ReturnPrice price = new ReturnPrice();

            if (priceEncarte.SalePrice > 0)
            {
                price = new ReturnPrice
                {
                    ProductId = returnPrice.ProductId,
                    SalePrice = priceEncarte.SalePrice,
                    MaximumPrice = returnPrice.SalePrice,
                    DiscountType = DiscountType.Encarte,
                    PercentageDiscount = Math.Round(((returnPrice.SalePrice - priceEncarte.SalePrice) / returnPrice.SalePrice) * 100, 2)
                };
            }

            return price;
        }

        private MedalDiscount GetMedalDiscount(long productId, long storeId, MedalCode medalCode)
        {
            MedalDiscount medalDiscount = _precoRepository.GetMedalDiscount(productId, storeId, medalCode);

            return medalDiscount;
        }
    }
}