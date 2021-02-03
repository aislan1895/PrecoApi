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
    public class PrecoController : ControllerBase
    {
        private readonly ILogger<PrecoController> _logger;
        private readonly IProductPriceService _productPriceService;

        public PrecoController(ILogger<PrecoController> logger, IProductPriceService productPriceService)
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
                    
                    if (customerScore.Score.Description == CodeMedal.Ouro.ToString().ToUpper())
                    {
                        returnPriceList.Add(_productPriceService.GetPriceOuroAndSenior(baseReturnprice, request.StoreId, CodeMedal.Ouro));
                    }
                    else if (customerScore.Score.Description == CodeMedal.Senior.ToString().ToUpper())
                    {
                        returnPriceList.Add(_productPriceService.GetPriceOuroAndSenior(baseReturnprice, request.StoreId, CodeMedal.Senior));
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
