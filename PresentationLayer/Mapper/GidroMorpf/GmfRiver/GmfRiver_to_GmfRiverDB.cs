using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfRiver_to_GmfRiverDB : Profile
    {
        public GmfRiver_to_GmfRiverDB() 
        {
            CreateMap<GmfRiver, GmfRiverDB>().ReverseMap();
        }
    }
}
