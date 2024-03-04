using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfClass3DB_to_GmfClass3BL : Profile
    {
        public GmfClass3DB_to_GmfClass3BL() 
        {
            CreateMap<GmfClass3DB, GmfClass3BL>();
        }
    }
}
