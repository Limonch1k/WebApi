using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfZonaDB_to_GmfZonaBL : Profile
    {
        public GmfZonaDB_to_GmfZonaBL() 
        {
            CreateMap<GmfZonaDB, GmfZonaBL>();
        }
    }
}
