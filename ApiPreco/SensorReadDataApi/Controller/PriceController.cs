using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrecoApi.Domain;
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
                List<BestPriceReturn> bestPrice = _productPriceService.GetPrice(request);

                if (bestPrice == null)
                    return NotFound();

                return Ok(bestPrice);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
