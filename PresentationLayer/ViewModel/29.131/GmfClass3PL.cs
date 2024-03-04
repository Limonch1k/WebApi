using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;

[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfClass3PL
{
    public short Class3 { get; set; }

    public decimal? Np3 { get; set; }

    public decimal? Vp3 { get; set; }

    public string? Sost3 { get; set; }

    public string Color3 { get; set; }
}
