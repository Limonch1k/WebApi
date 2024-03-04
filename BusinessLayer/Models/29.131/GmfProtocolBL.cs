using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Models;

public partial class GmfProtocolBL
{
    public int Kn { get; set; }

    public int God { get; set; }

    public int? Pcat { get; set; }

    public string Pindicator { get; set; } = null!;

    public string? Aestimation { get; set; }

    public string? Bestimation { get; set; }

    public bool? MarkEstam { get; set; }

    public string? Prim { get; set; }

    public string? Opisanie { get; set; }

      public DateTime? Date {get;set;}

    public virtual GmfPunctBL KnNavigation { get; set; } = null!;

    public virtual GmfIndicatorBL PindicatorNavigation { get; set; } = null!;
}
