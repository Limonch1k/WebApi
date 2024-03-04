using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace api_fact_weather_by_city.ViewModel;

[SwaggerSchemaFilter(typeof(SwaggerSchemaCustomFilter))]
public partial class GmfZonaPL
{
    public short Nz { get; set; }

    public string? Namezoa { get; set; }

    public string? Grzona { get; set; }

    public virtual ICollection<GmfCategoryPL> GmfCategories { get; } = new List<GmfCategoryPL>();
}
