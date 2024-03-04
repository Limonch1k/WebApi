using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfRiverDB_to_GmfRiverBL : Profile
    {
        public GmfRiverDB_to_GmfRiverBL() 
        {
            CreateMap<GmfRiverDB, GmfRiverBL>().ReverseMap();
        }
    }
}