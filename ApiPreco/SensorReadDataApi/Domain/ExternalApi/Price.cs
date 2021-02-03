using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecoApi.Domain.ExternalApi
{
    public class Price
    {
        private int subsidiaryId;
        private decimal SalePrice;
        private decimal EverBluePrice;
        private decimal ReferencePrince;
        private decimal ListPrice;
        private decimal MaxPrice;

        public string id
        {
            get => subsidiaryId.ToString();
            set
            {
                if (value != null)
                    subsidiaryId = Convert.ToInt32(value);
                else
                    subsidiaryId = 0;
            }
        }

        public string salePrice
        {
            get => SalePrice.ToString();
            set
            {
                if (value != null)
                    SalePrice = Convert.ToDecimal(value.Replace(".", ","));
                else
                    SalePrice = 0;
            }
        }

        public string everBluePrice
        {
            get => EverBluePrice.ToString();
            set
            {
                if (value != null)
                    EverBluePrice = Convert.ToDecimal(value.Replace(".", ","));
                else
                    EverBluePrice = 0;
            }
        }

        public string referencePrince
        {
            get => ReferencePrince.ToString();
            set
            {
                if (value != null)
                    ReferencePrince = Convert.ToDecimal(value.Replace(".", ","));
                else
                    ReferencePrince = 0;
            }
        }

        public string listPrice
        {
            get => ListPrice.ToString();
            set
            {
                if (value != null)
                    ListPrice = Convert.ToDecimal(value.Replace(".", ","));
                else
                    ListPrice = 0;
            }
        }

        public string maxPrice
        {
            get => MaxPrice.ToString();
            set
            {
                if (value != null)
                    MaxPrice = Convert.ToDecimal(value.Replace(".", ","));
                else
                    MaxPrice = 0;
            }
        }
    }
}
