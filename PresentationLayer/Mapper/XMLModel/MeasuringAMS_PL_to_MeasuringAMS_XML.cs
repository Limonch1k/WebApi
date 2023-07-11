using api_fact_weather_by_city.XMLModels;
using AutoMapper;
using DB.TableModels;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class MeasuringAMS_PL_to_MeasuringAMS_XML : Profile
    {
        public MeasuringAMS_PL_to_MeasuringAMS_XML()
        {
            CreateMap<MeasuringAMS_PL, MeasuringAMS_XML>()
            //.ForMember(pl => pl.DateTime, src => src.MapFrom(xml => DateTime.Equals(xml.DateTime, new DateTime()) ? xml.DateTime.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture) : "null"))
            //.ForMember(pl => pl.DateWrite, src => src.MapFrom(xml=> DateTime.Equals(xml.DateWrite, new DateTime()) ? xml.DateWrite.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture) : "null"));
            .ForMember(pl => pl.DateTime, src => src.MapFrom(xml => xml.DateTime.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ?  "NULL" : xml.DateTime.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)))
            .ForMember(pl => pl.DateWrite, src => src.MapFrom(xml => xml.DateWrite.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : xml.DateWrite.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)));
        }
    }
}