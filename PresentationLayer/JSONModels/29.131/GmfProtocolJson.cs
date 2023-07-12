using api_fact_weather_by_city.ViewModel;
using Newtonsoft.Json;

namespace api_fact_weather_by_city.JSONModels._29._131
{
    public class GmfProtocolJson
    {

        public GmfProtocolJson() 
        {
            Pcat = -1;
            Aestimation = null;
            Bestamination = null;
            MarkEstam = null;
            Prim = null;
            Оpisanie = null;
        }

        public int Kn { get; set; }

        public int God { get; set; }

        public int Pcat { get; set; }

        public string Pindicator { get; set; } = null!;

        public string? Aestimation { get; set; }

        public string? Bestamination { get; set; }

        public bool? MarkEstam { get; set; }

        public string? Prim { get; set; }

        public string? Оpisanie { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Include)]
        //public virtual GmfPunctJson KnNavigationJson { get; set; } = null!;

        //[JsonProperty(NullValueHandling = NullValueHandling.Include)]
        //public virtual GmfIndicatorJson PindicatorNavigation { get; set; } = null!;
    }
}
