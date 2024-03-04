using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfUpdateRiver_to_GmfRiverBL : Profile
    {
        public GmfUpdateRiver_to_GmfRiverBL() 
        {
            CreateMap<GmfUpdateRiver, GmfRiverBL>();
            
        }
    }
}
