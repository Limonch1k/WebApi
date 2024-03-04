using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{ 
    public class GmfClass3BL_to_GmfClass3PL : Profile
    {
        public GmfClass3BL_to_GmfClass3PL() 
        {
            CreateMap<GmfClass3BL, GmfClass3PL>().ReverseMap();
        }

    }
}
