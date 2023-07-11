using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfProtocol
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

    public virtual GmfPunct KnNavigation { get; set; } = null!;

    public virtual GmfIndicator PindicatorNavigation { get; set; } = null!;
}
