using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfClass5BL_to_GmfClass5PL : Profile
    {
        public GmfClass5BL_to_GmfClass5PL() 
        {
            CreateMap<GmfClass5BL, GmfClass5PL>().ReverseMap();
        }
    }
}
