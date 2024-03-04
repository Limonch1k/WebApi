using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLayer.TableModels;

public class GmfCategory
{
    public short Idcat { get; set; }

    public string? Namecat { get; set; }

    public short? Nzona { get; set; }

    public virtual ICollection<GmfIndicator> GmfIndicators { get; set; } = new List<GmfIndicator>();
    public virtual GmfZona NzonaNavigation { get; set; } = null!;
}
