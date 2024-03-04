using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfIndicatorDB_to_GmfIndicatorBL : Profile
    {
        public GmfIndicatorDB_to_GmfIndicatorBL() 
        {
            CreateMap<GmfIndicatorDB, GmfIndicatorBL>();
        }
    }
}
