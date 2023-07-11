using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfCategoryDB_to_GmfCategoryBL : Profile
    {
        public GmfCategoryDB_to_GmfCategoryBL()
        {
            CreateMap<GmfCategoryDB, GmfCategoryBL>();
        }
    }
}
