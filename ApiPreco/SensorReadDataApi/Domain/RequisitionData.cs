using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain
{
    public class RequisitionData
    {
        public string CpfCnpjCustomer { get; set; }

        public long StoreId { get; set; }

        public List<Product> Products { get; set; }
    }
}
