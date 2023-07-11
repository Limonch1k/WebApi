using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralObject.MyCustomAttribute;

namespace BusinessLayer.ParametrModel
{
    public class MeteoParamModel_BL 
    {
        [RenamePropertyAttribute(PropertyName = "ResourceId")]
        [OrderbyAttribute(Order = 1)]
        public string[] stationList { get; set; }

        public void SetStationList(string stationId) 
        {
            if (stationId is not null)
            {
                this.stationList = stationId.Split(",");
            }
            else
            {
                this.stationList = null;
            }
        }

        [RenamePropertyAttribute(PropertyName = "Param")]
        [OrderbyAttribute(Order = 2)]
        public string[] paramList { get; set; }

        public void SetParamList(string paramList) 
        {
            if (paramList is not null)
            {
                this.paramList = paramList.Split(",");
            }
            else
            {
                this.paramList = null;
            }
        }

        [RenamePropertyAttribute(PropertyName = "Orderby")]
        [OrderbyAttribute(Order = 5)]
        public string[] orderbyList { get; set; }

        public void SetOrderByList(string orderby) 
        {
            if (orderby is not null)
            {
                this.orderbyList = orderby.Split(",");
            }
            else
            {
                this.orderbyList = null;
            }
        }

        [RenamePropertyAttribute(PropertyName = "StartDate")]
        [OrderbyAttribute(Order = 3)]
        public DateTime? start_dt { get; set; }


        [RenamePropertyAttribute(PropertyName = "EndDate")]
        [OrderbyAttribute(Order = 4)]
        public DateTime? end_dt { get; set; }

        public void SetDateList(string start_dt, string end_dt) 
        {
            if (start_dt is null && end_dt is null)
            {
                this.start_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, DateTime.Now.Hour, 00, 00);
                this.end_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 00, 00);
            }

            if (start_dt is null)
            {
                this.end_dt = DateTime.ParseExact(end_dt, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                this.start_dt = new DateTime(this.end_dt.Value.Year, this.end_dt.Value.Month, this.end_dt.Value.Day - 1, this.end_dt.Value.Hour, 00, 00);
            }

            if (end_dt is null)
            {
                this.start_dt = DateTime.ParseExact(start_dt, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                this.end_dt = new DateTime(this.start_dt.Value.Year, this.start_dt.Value.Month, this.start_dt.Value.Day + 1, this.start_dt.Value.Hour, 00, 00);
            }
            else
            {
                this.start_dt = DateTime.ParseExact(start_dt, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                this.end_dt = DateTime.ParseExact(end_dt, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        [RenamePropertyAttribute(PropertyName = "ModelId")]
        [OrderbyAttribute(Order = 6)]
        public string model_id { get; set; }

        public void SetModelId(string model_id) 
        {
            this.model_id = model_id;
        }

    }
}
