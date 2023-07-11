using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfEstaminationDB_to_GmfEstaminationBL : Profile
    {
        public GmfEstaminationDB_to_GmfEstaminationBL() 
        {
            CreateMap<GmfEstaminationDB, GmfEstaminationBL>();
        }
    }
}
