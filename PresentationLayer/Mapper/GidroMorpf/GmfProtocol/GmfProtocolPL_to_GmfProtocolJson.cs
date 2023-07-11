using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using BusinessLayer.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper.GidroMorpf.GmfProtocol
{
    public class GmfProtocolPL_to_GmfProtocolJson : Profile
    {
        public GmfProtocolPL_to_GmfProtocolJson() 
        {
            CreateMap<GmfProtocolPL, GmfProtocolJson>().ReverseMap();
        }
    }
}
