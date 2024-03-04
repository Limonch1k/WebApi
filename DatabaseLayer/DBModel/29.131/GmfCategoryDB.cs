using DatabaseLayer.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DBModel;

public class GmfCategoryDB
{
    public short Idcat { get; set; }

    public string? Namecat { get; set; }

    public short Nzona { get; set; }

    public virtual ICollection<GmfIndicatorDB> GmfIndicators { get; } = new List<GmfIndicatorDB>();

    public virtual GmfZonaDB NzonaNavigation { get; set; } = null!;
}

