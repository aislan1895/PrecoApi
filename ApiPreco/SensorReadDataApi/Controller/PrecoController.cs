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

        [HttpGet]
        public IActionResult GetBestPrice([FromBody] RequisitionData request)
        {
            try
            {
                /*
                 Identificar a medalha do cliente.
                 Identificar o percentual de desconto da medalha para aquele produto
                 Pegar o preço da medalha e preço segmentado caso seja Ouro ou Senior
                 Chamar api de preço base caso seja Azul                 
                 */





                //    List<Product> products = new List<Product>();
                //    products.Add(new Product { Id = 10 });
                //    products.Add(new Product { Id = 11 });
                //    Request req = new Request
                //    {
                //        CpfcnpjCustomer = "1234567",
                //        StoreId = 100,
                //        Products = products
                //    };
                //    var json = new JavaScriptSerializer().Serialize(req);
                //    Console.WriteLine(json);
                             
                var data = new BestPriceReturn();

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter dados");
                return new StatusCodeResult(500);
            }
        }

        //[HttpPost]
        //public IActionResult SetData([FromBody]long step)
        //{
        //    try
        //    {
        //        var result = _sensorRepository.Insert(step);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao tentar inserir dados");
        //        return new StatusCodeResult(500);
        //    }
        //}
    }
}