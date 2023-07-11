using api_fact_weather_by_city.ViewModel;
using api_fact_weather_by_city.XMLModels;
using AutoMapper;
using DB.TableModels;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GroundData_PL_to_GroundData_XML : Profile
    {
        public GroundData_PL_to_GroundData_XML()
        {
            CreateMap<GroundData_PL, GroundDataXML>()
            .ForMember(pl => pl.DateObservation, src => src.MapFrom(xml => xml.Datas.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : xml.Datas.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)))
            .ForMember(pl => pl.DateWrite, src => src.MapFrom(xml => ((DateTime)(xml.DateWrite)).ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : ((DateTime)(xml.DateWrite)).ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)));

        }
    }
}