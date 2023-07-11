using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfEstaminationBL_to_GmfEstaminationPL : Profile
    {
        public GmfEstaminationBL_to_GmfEstaminationPL() 
        {
            CreateMap<GmfIndicatorBL, GmfIndicatorPL>();
        }
    }
}
