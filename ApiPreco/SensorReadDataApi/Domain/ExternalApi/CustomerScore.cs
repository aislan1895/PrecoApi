using System;
using System.Threading.Tasks;

namespace PrecoApi.Domain.ExternalApi
{
    public class CustomerScore    {
        
        public Customer Customer { get; set; }
        public Score Score { get; set; }
        public FurtherDetail FurtherDetail { get; set; }

        public static explicit operator CustomerScore(Task<CustomerScore> v)
        {
            throw new NotImplementedException();
        }
    }
}
