using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper;
public class GmfClass3_to_GmfClass3DB : Profile
{
    public GmfClass3_to_GmfClass3DB()
    {
        CreateMap<GmfClass3, GmfClass3DB>();
    }
}

