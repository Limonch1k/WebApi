using DatabaseLayer.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models;


public class GmfCategoryBL
{
    public short Idcat { get; set; }

    public string? Namecat { get; set; }

    public short Nzona { get; set; }

    public virtual ICollection<GmfIndicatorBL> GmfIndicators { get; } = new List<GmfIndicatorBL>();

    public virtual GmfZonaBL NzonaNavigation { get; set; } = null!;
}
