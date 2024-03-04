using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfProtocolBL_to_GmfProtocalPL : Profile
    {
        public GmfProtocolBL_to_GmfProtocalPL() 
        {
            CreateMap<GmfProtocolBL, GmfProtocolPL>().ReverseMap();
        }
    }
}
