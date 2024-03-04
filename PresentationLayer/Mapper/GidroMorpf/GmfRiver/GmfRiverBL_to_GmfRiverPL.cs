using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfRiverBL_to_GmfRiverPL : Profile
    {
        public GmfRiverBL_to_GmfRiverPL() 
        {
            CreateMap<GmfRiverBL, GmfRiverPL>().ReverseMap();
        }
    }
}
