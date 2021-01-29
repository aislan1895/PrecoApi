using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrecoApi.Domain;
using PrecoApi.Domain.ExternalApi;
using PrecoApi.Repository;

namespace PrecoApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoController : ControllerBase
    {
        private readonly ILogger<PrecoController> _logger;
        private readonly IPrecoRepository _precoRepository;

        public PrecoController(ILogger<PrecoController> logger, IPrecoRepository precoRepository)
        {
            _logger = logger;
            _precoRepository = precoRepository;
        }

//        [HttpGet]
//        public IActionResult GetBestPrice([FromBody] RequisitionData request)
//        {
//            try
//            {
//                //ExternalAccessController externalAccessController = new ExternalAccessController();

//                //var customer = externalAccessController.GetCustomerScoreAsync(request.CpfCnpjCustomer);                
            
//.                /*
//                 Identificar a medalha do cliente.
//                 Identificar o percentual de desconto da medalha para aquele produto
//                 Pegar o preço da medalha e preço segmentado caso seja Ouro ou Senior
//                 Chamar api de preço base caso seja Azul                 
//                 */



//                //    List<Product> products = new List<Product>();
//                //    products.Add(new Product { Id = 10 });
//                //    products.Add(new Product { Id = 11 });
//                //    Request req = new Request
//                //    {
//                //        CpfcnpjCustomer = "1234567",
//                //        StoreId = 100,
//                //        Products = products
//                //    };
//                //    var json = new JavaScriptSerializer().Serialize(req);
//                //    Console.WriteLine(json);
//                List<Price> priceList = new List<Price>();

//                var data = GetPriceController.GetBestPrice(priceList);

//                return Ok(data);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Erro ao tentar obter dados");
//                return new StatusCodeResult(500);
//            }
//        }

        [HttpGet]
        public IActionResult GetBestPrice([FromBody] RequisitionData request)
        {
            try
            {
                ExternalAccessController externalAccessController = new ExternalAccessController();
                List<PriceReturn> priceReturnList = new List<PriceReturn>();
                PriceReturn priceReturn; 

                CustomerScore customerScore = externalAccessController.GetCustomerScoreAsync(request.CpfCnpjCustomer).Result;

                Price priceBased = externalAccessController.GetPriceOuroAsync(request.Products[0].Id.ToString(), request.StoreId.ToString()).Result;

                priceReturn = new PriceReturn {
                    DiscountType = "Azul",
                    MaximumPrice = Decimal.Parse(priceBased.maxPrice),
                    PercentageDiscount = 0,
                    ProductId = request.Products[0].Id,
                    SalePrice = Decimal.Parse(priceBased.salePrice)
                };

                priceReturnList.Add(priceReturn);

                if (customerScore.Score.id == "2")
                {
                    priceReturn = new PriceReturn
                    {
                        DiscountType = "Ouro",
                        MaximumPrice = Decimal.Parse(priceBased.maxPrice),
                        PercentageDiscount = 5,
                        ProductId = request.Products[0].Id,
                        SalePrice = Decimal.Parse(priceBased.salePrice) - Decimal.Multiply(Decimal.Parse(priceBased.salePrice), Decimal.Parse("0,05"))
                    };
                }
                priceReturnList.Add(priceReturn);                

                var data = GetPriceController.GetBestPrice(priceReturnList);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter dados");
                return new StatusCodeResult(500);
            }
        }
    }
}
