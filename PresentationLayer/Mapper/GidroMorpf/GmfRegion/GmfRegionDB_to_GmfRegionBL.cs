using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfRegionDB_to_GmfRegionBL : Profile
    {
        public GmfRegionDB_to_GmfRegionBL() 
        {
            CreateMap<GmfRegionDB, GmfRegionBL>().ReverseMap();
        }
    }
}
