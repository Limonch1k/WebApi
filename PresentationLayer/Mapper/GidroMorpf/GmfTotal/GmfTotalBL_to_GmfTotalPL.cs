using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfTotalBL_to_GmfTotalPL : Profile
    {
        public GmfTotalBL_to_GmfTotalPL() 
        {
            CreateMap<GmfTotalBL,GmfTotalPL>().ReverseMap();
        }
    }
}
