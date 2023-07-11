using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper;
public class GmfClass5DB_to_GmfClass5BL : Profile
{
    public GmfClass5DB_to_GmfClass5BL() 
    {
        CreateMap<GmfClass5DB, GmfClass5BL>();
    }
}

