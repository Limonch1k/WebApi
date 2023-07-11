using System;
using System.Collections.Generic;

namespace api_fact_weather_by_city.ViewModel;

public partial class GmfProtocolPL
{
    public int Kn { get; set; }

    public int God { get; set; }

    public int Pcat { get; set; }

    public string Pindicator { get; set; } = null!;

    public string? Aestimation { get; set; }

    public string? Bestamination { get; set; }

    public bool? MarkEstam { get; set; }

    public string? Prim { get; set; }

    public string? Оpisanie { get; set; }

    public virtual GmfPunctPL KnNavigation { get; set; } = null!;

    public virtual GmfIndicatorPL PindicatorNavigation { get; set; } = null!;
}
