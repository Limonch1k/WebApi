using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;

[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfClass5PL
{
    public short Class5 { get; set; }

    public decimal? Np5 { get; set; }

    public decimal? Vp5 { get; set; }

    public string? Sost5 { get; set; }

    public string Color5 { get; set; }
}
