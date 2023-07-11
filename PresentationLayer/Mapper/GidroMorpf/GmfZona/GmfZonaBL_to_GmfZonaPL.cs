using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfZonaBL_to_GmfZonaPL : Profile
    {
        public GmfZonaBL_to_GmfZonaPL() 
        {
            CreateMap<GmfZonaBL, GmfZonaPL>();
        }
    }
}
