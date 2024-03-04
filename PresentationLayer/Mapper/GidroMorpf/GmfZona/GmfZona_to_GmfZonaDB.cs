using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfZona_to_GmfZonaDB : Profile
    {
        public GmfZona_to_GmfZonaDB() 
        {
            CreateMap<GmfZona, GmfZonaDB>();
        }
    }
}
