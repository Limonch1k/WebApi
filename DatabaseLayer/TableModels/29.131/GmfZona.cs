using System;
using System.Collections.Generic;

namespace DatabaseLayer.TableModels;

public class GmfZona
{
    public short Nz { get; set; }

    public string? Namezoa { get; set; }

    public string? Grzona { get; set; }

    public virtual ICollection<GmfCategory> GmfCategories { get; } = new List<GmfCategory>();
}
