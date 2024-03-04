using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfIndicator
{
    public short Cat { get; set; }

    public string Idindicator { get; set; } = null!;

    public string? Namindicator { get; set; }

    public bool? Idmain { get; set; }

    public bool? Iddop { get; set; }

    public bool? Mark { get; set; }

    public short? GroupIndic { get; set; }

    public short? Zona_indic {get;set;}

    public GmfCategory CatNavigation { get; set; } = null!;

    public List<GmfProtocol> GmfProtocols { get; set; } = new List<GmfProtocol>();
}
