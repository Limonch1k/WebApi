using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfClass5_to_GmfClass5DB : Profile
    {
        public GmfClass5_to_GmfClass5DB() 
        {
            CreateMap<GmfClass5, GmfClass5DB>();
        }
    }
}
