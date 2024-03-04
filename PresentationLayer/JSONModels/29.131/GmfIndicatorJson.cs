using api_fact_weather_by_city.ViewModel;
using Newtonsoft.Json;

namespace api_fact_weather_by_city.JSONModels._29._131
{
    public class GmfIndicatorJson
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public short Cat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Idindicator { get; set; } = null!;
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Namindicator { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Idmain { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Iddop { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public bool? Mark { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public short? GroupIndic { get; set; }

        //ublic virtual GmfCategoryPL CatNavigation { get; set; } = null!;

        //public virtual ICollection<GmfProtocolPL> GmfProtocols { get; } = new List<GmfProtocolPL>();
    }
}
