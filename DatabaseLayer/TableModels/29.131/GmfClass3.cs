using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfClass3
{
    public int Class3 { get; set; }

    public decimal? Np3 { get; set; }

    public decimal? Vp3 { get; set; }

    public string? Sost3 { get; set; }

    public string Color3 { get; set; }

    public virtual List<GmfTotal> gmfTotal {get;set;} = null;
}
