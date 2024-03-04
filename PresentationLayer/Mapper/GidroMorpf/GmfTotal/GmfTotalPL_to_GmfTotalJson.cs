using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfTotalPL_to_GmfTotalJson : Profile
    {
        public GmfTotalPL_to_GmfTotalJson() 
        {
            CreateMap<GmfTotalPL, GmfTotalJson>().ReverseMap();
        }
    }
}
