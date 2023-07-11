using DatabaseLayer.DBModel;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models;

public partial class GmfZonaBL
{
    public short Nz { get; set; }

    public string? Namezoa { get; set; }

    public string? Grzona { get; set; }

    public virtual ICollection<GmfCategoryBL> GmfCategories { get; } = new List<GmfCategoryBL>();
}
