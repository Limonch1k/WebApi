using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfProtocol
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

    public virtual GmfPunct KnNavigation { get; set; } = null!;

    public virtual GmfIndicator PindicatorNavigation { get; set; } = null!;
}
