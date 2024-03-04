using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfPunctDB_to_GmfPunctBL : Profile
    {
        public GmfPunctDB_to_GmfPunctBL() 
        {
            CreateMap<GmfPunctDB, GmfPunctBL>().ReverseMap();
        }
    }
}
