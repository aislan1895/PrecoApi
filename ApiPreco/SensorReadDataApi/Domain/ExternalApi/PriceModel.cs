using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain.ExternalApi
{
    public class PriceModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public Price price { get; set; }
    }
}
