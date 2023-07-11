using System;
using System.Collections.Generic;

namespace api_fact_weather_by_city.ViewModel;

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
