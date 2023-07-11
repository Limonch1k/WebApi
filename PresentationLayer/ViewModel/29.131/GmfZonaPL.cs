using System;
using System.Collections.Generic;

namespace api_fact_weather_by_city.ViewModel;

public partial class GmfZonaPL
{
    public short Nz { get; set; }

    public string? Namezoa { get; set; }

    public string? Grzona { get; set; }

    public virtual ICollection<GmfCategoryPL> GmfCategories { get; } = new List<GmfCategoryPL>();
}
