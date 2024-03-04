using System;
using System.Collections.Generic;

namespace DatabaseLayer.DBModel;

public partial class GmfZonaDB
{
    public short Nz { get; set; }

    public string? Namezoa { get; set; }

    public string? Grzona { get; set; }

    public virtual ICollection<GmfCategoryDB> GmfCategories { get; } = new List<GmfCategoryDB>();
}
