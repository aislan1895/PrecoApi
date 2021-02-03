namespace PrecoApi.Domain.ExternalApi
{
    public class CustomerScoreModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public CustomerScore customerScore { get; set; }
    }
}
