using System;
using System.Collections.Generic;

namespace DatabaseLayer.DBModel;

public partial class GmfIndicatorDB
{
    public short Cat { get; set; }

    public string Idindicator { get; set; } = null!;

    public string? Namindicator { get; set; }

    public bool? Idmain { get; set; }

    public bool? Iddop { get; set; }

    public bool? Mark { get; set; }

    public short? GroupIndic { get; set; }

    public virtual GmfCategoryDB CatNavigation { get; set; } = null!;

    public virtual ICollection<GmfProtocolDB> GmfProtocols { get; } = new List<GmfProtocolDB>();
}
