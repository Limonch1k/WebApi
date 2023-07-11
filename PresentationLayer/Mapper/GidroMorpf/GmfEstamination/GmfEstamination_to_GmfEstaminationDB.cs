using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfEstamination_to_GmfEstaminationDB : Profile
    {
        public GmfEstamination_to_GmfEstaminationDB() 
        {
            CreateMap<GmfEstamination, GmfEstaminationDB>();
        }
    }
}
