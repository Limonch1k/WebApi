using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfTotal_to_GmfTotalDB : Profile
    {
        public GmfTotal_to_GmfTotalDB() 
        {
            CreateMap<GmfTotal, GmfTotalDB>().ReverseMap();
        }
    }
}
