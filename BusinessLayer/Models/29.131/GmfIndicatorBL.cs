using DatabaseLayer.DBModel;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models;

public partial class GmfIndicatorBL
{
    public short Cat { get; set; }

    public string Idindicator { get; set; } = null!;

    public string? Namindicator { get; set; }

    public bool? Idmain { get; set; }

    public bool? Iddop { get; set; }

    public bool? Mark { get; set; }

    public short? GroupIndic { get; set; }

    public virtual GmfCategoryBL CatNavigation { get; set; } = null!;

    public virtual ICollection<GmfProtocolBL> GmfProtocols { get; } = new List<GmfProtocolBL>();
}
