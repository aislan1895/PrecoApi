using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain.ExternalApi
{
    public class Score
    {
        public string id { get; set; }
        public string description { get; set; }
        public string initialConsumptionRange { get; set; }
        public string finalConsumptionRange { get; set; }
    }
}
