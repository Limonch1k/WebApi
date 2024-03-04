using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfPunctBL_to_GmfPunctPL : Profile
    {
        public GmfPunctBL_to_GmfPunctPL() 
        {
            CreateMap<GmfPunctBL, GmfPunctPL>().ReverseMap();
        }
    }
}
