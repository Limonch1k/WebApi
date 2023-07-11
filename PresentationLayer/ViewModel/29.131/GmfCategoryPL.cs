using System;
using System.Collections.Generic;

namespace api_fact_weather_by_city.ViewModel;

public partial class GmfCategoryPL
{
    public short Idcat { get; set; }

    public string? Namecat { get; set; }

    public short Nzona { get; set; }

    public virtual ICollection<GmfIndicatorPL> GmfIndicators { get; } = new List<GmfIndicatorPL>();

    public virtual GmfZonaPL NzonaNavigation { get; set; } = null!;
}
