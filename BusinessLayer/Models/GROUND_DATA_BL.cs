using GeneralObject.MyCustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public partial class GroundData_BL
    {
        [DateObservationAttribute]
        public DateTime Datas { get; set; }

        [StationIdAttribute]
        public int ResourceId { get; set; }

        public decimal AdvanceTime { get; set; }

        public decimal ModelId { get; set; }

        public decimal? WindU { get; set; }

        public decimal? WindV { get; set; }

        public decimal? Pressure { get; set; }

        public decimal? Temp { get; set; }

        public decimal? RHumid { get; set; }

        public decimal? PrecipTotal { get; set; }

        public decimal? Visib { get; set; }

        public decimal? Winddirection { get; set; }

        [DateWriteAttribute]
        public DateTime? DateWrite { get; set; }
    }
}
