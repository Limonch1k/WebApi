using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;

[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfPunctPL
{
    public int Kod { get; set; }

    public string? Bass { get; set; }

    public string? River { get; set; }

    public string? Pasp { get; set; }

    public string? Region { get; set; }

    public string? Punkt { get; set; }

    public virtual ICollection<GmfProtocolPL> GmfProtocols { get; } = new List<GmfProtocolPL>();
}
