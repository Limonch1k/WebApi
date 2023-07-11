using GeneralObject.MyCustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.ParamModel
{
    public class MeteoParamModel_DL
    {
 
        [OrderbyAttribute(Order = 1)]
        public string[] stationList { get; set; }

        [OrderbyAttribute(Order = 2)]
        public string[] paramList { get; set; }

        [OrderbyAttribute(Order = 5)]
        public string[] orderbyList { get; set; }

        [OrderbyAttribute(Order = 3)]
        public DateTime? start_dt { get; set; }

        [OrderbyAttribute(Order = 4)]
        public DateTime? end_dt { get; set; }
    
        [OrderbyAttribute(Order = 6)]
        public string model_id { get; set; }
    }
}
