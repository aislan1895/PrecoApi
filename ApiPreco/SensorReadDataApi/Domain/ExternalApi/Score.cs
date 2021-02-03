using System.Text.Json.Serialization;

namespace PrecoApi.Domain.ExternalApi
{
    public class Score
    {
        [JsonPropertyName ("id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("initialConsumptionRange")]
        public string InitialConsumptionRange { get; set; }

        [JsonPropertyName("finalConsumptionRange")]
        public string FinalConsumptionRange { get; set; }
    }
}
