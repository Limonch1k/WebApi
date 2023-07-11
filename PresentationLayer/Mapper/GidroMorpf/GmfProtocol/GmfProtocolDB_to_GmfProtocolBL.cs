using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfProtocolDB_to_GmfProtocolBL : Profile
    {
        public GmfProtocolDB_to_GmfProtocolBL() 
        {
            CreateMap<GmfProtocolDB, GmfProtocolBL>().ReverseMap();
        }
    }
}
