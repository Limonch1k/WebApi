using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfClass5
{
    public int Class5 { get; set; }

    public decimal? Np5 { get; set; }

    public decimal? Vp5 { get; set; }

    public string? Sost5 { get; set; }

    public string Color5 { get; set; }

    public List<GmfTotal> gmfTotal {get;set;} = null;
}
