using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfIndicatorBL_to_GmfIndicatorPL : Profile
    {
        public GmfIndicatorBL_to_GmfIndicatorPL() 
        {
            CreateMap<GmfIndicatorBL, GmfIndicatorPL>();
        }
    }
}
