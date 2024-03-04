using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;
[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfEstaminationPL
{
    public string? Estam { get; set; }

    public short Np { get; set; }
}
