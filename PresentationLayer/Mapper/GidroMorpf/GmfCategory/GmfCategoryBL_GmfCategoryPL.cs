using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfCategoryBL_GmfCategoryPL : Profile
    {
        public GmfCategoryBL_GmfCategoryPL()
        {
            CreateMap<GmfCategoryBL, GmfCategoryPL>();
        }
    }
}
