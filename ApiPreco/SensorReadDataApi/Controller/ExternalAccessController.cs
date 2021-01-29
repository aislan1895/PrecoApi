using PrecoApi.Domain.ExternalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrecoApi.Controller
{
    public class ExternalAccessController
    {
        HttpClient httpClient = new HttpClient();
        public async Task<CustomerScore> GetCustomerScoreAsync(string cpfCnpj)
        {
            CustomerScoreModel customerScoreModel = await new RequestServices<CustomerScoreModel>(httpClient).SendResquest($"https://dev.apipmenos.com/customer/v1/score/" + cpfCnpj,
                                                                                                                                               "4IBFLIlhDo4Uo9wXMGLd22JxPIax3DZwaNMSnK5w");
            
            return customerScoreModel.customerScore;
        }

        public async Task<Price> GetPriceOuroAsync(string productId, string storeId)
        {
            PriceModel priceModel = await new RequestServices<PriceModel>(httpClient).SendResquest($"https://dev.apipmenos.com/price/v1/"+productId+"?subsidiaryId="+storeId,
                                                                                                                                                "vhubPbOuqb7X5ZEuflnJN1c3GlR03K2x4KzAt6d1");

            return priceModel.price;
        }

    }


}
