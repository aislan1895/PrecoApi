using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using PrecoApi.Domain.ExternalApi;
using PrecoApi.Service.Interface;
using System;
using System.Collections.Generic;

namespace PrecoApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IProductPriceService _productPriceService;

        public PriceController(ILogger<PriceController> logger, IProductPriceService productPriceService)
        {
            _logger = logger;
            _productPriceService = productPriceService;
        }

        [HttpGet]
        public IActionResult GetBestPrice([FromBody] RequisitionData request)
        {
            try
            {                
                List<BestPriceReturn> bestPriceReturnList = new List<BestPriceReturn>();

                CustomerScore customerScore = _productPriceService.GetCustomerScoreAsync(request.CpfCnpjCustomer).Result;

                foreach (Product product in request.Products)
                {
                    List<ReturnPrice> returnPriceList = new List<ReturnPrice>();
                    ReturnPrice baseReturnprice = _productPriceService.GetPriceAzulAsync(product.Id.ToString(), request.StoreId.ToString()).Result;
                    returnPriceList.Add(baseReturnprice);

                    ReturnPrice priceEncarte = _productPriceService.GetPriceEncarte(baseReturnprice, request.StoreId, (MedalCode)int.Parse(customerScore.Score.Id));

                    if (priceEncarte.SalePrice > 0)
                    {
                        returnPriceList.Add(priceEncarte);
                    }
                    
                    if (customerScore.Score.Description == MedalCode.Ouro.ToString().ToUpper())
                    {
                        returnPriceList.Add(_productPriceService.GetPriceOuro(baseReturnprice, request.StoreId, MedalCode.Ouro));
                    }
                    else if (customerScore.Score.Description == MedalCode.Senior.ToString().ToUpper())
                    {
                        returnPriceList.Add(_productPriceService.GetPriceSenior(baseReturnprice, request.StoreId, MedalCode.Senior));
                    }

                    bestPriceReturnList.Add(_productPriceService.GetBestPrice(returnPriceList));
                }                

                return Ok(bestPriceReturnList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter dados");
                return new StatusCodeResult(500);
            }
        }
    }
}
