namespace PrecoApi.Domain.ExternalApi
{
    public class PriceModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public Price price { get; set; }
    }
}
