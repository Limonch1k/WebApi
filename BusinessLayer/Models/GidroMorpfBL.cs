using DatabaseLayer.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLModels
{
    public class GidroMorpfBL
    {
        public int Kod { get; set; }

        public string? Bass { get; set; }

        public string? River { get; set; }

        public string? Pasp { get; set; }

        public string? Region { get; set; }

        public string? Reestr { get; set; }

        public bool? Trans { get; set; }

        public string? Punkt { get; set; }

        public decimal? Lon { get; set; }

        public decimal? Lat { get; set; }

        public bool? Gmf { get; set; }

        public bool? Gx { get; set; }
    }
}
