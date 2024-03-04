using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfRegion_to_GmfRegionBD : Profile
    {
        public GmfRegion_to_GmfRegionBD() 
        {
            CreateMap<GmfRegion, GmfRegionDB>().ReverseMap();
        }
    }
}
