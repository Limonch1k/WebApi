using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;
[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfIndicatorPL
{
    public short Cat { get; set; }

    public string Idindicator { get; set; } = null!;

    public string? Namindicator { get; set; }

    public bool? Idmain { get; set; }

    public bool? Iddop { get; set; }

    public bool? Mark { get; set; }

    public short? GroupIndic { get; set; }

    public virtual GmfCategoryPL CatNavigation { get; set; } = null!;

    public virtual ICollection<GmfProtocolPL> GmfProtocols { get; } = new List<GmfProtocolPL>();
}
