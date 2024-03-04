using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper.GidroMorpf.GmfProtocol
{
    public class GmfProtocolPL_to_GmfProtocolJson : Profile
    {
        Func<GmfProtocolPL, string> convertDateToString = js =>
        {
            string str = "";
            if (js.Date is null)
            {
                str = "NULL";
            }
            else
            {
                str = ((DateTime)(js.Date)).ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : ((DateTime)(js.Date)).ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            return str;
        };

        public GmfProtocolPL_to_GmfProtocolJson() 
        {
            CreateMap<GmfProtocolPL, GmfProtocolJson>()
            .ForMember(pl => pl.Date, src => src.MapFrom(js => convertDateToString(js)
            ));
        }
    }

    public class GmfProtocolJson_to_GmfProtocolPL : Profile
    {
        public GmfProtocolJson_to_GmfProtocolPL() 
        {
            CreateMap<GmfProtocolJson,GmfProtocolPL>()
            .ForMember(pl => pl.Date, src => src.MapFrom(js => DateTime.ParseExact(js.Date,"yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)));
        }
    }
}
