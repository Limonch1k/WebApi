using api_fact_weather_by_city.ViewModel;
using Newtonsoft.Json;

namespace api_fact_weather_by_city.JSONModels._29._131
{
    public class GmfPunctJson
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int Kod { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Bass { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? River { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Pasp { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Region { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Reestr { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Trans { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Punkt { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public decimal? Lon { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public decimal? Lat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Gmf { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Gx { get; set; }

        //public virtual ICollection<GmfProtocolPL> GmfProtocols { get; } = new List<GmfProtocolPL>();
    }
}
