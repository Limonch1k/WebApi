using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GroundData_to_GroundData_DB : Profile
    {
        public GroundData_to_GroundData_DB()
        {
            CreateMap<GroundDatum, GroundData_DB>();      
        }   
    }
}