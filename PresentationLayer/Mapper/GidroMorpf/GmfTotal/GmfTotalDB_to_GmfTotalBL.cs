using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfTotalDB_to_GmfTotalBL : Profile
    {
        public GmfTotalDB_to_GmfTotalBL() 
        {
            CreateMap<GmfTotalDB, GmfTotalBL>().ReverseMap();
        }
    }
}
