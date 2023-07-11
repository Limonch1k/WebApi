using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class MeasuringAMS_to_MeasuringAMS_DB : Profile
    {
        public MeasuringAMS_to_MeasuringAMS_DB()
        {
            CreateMap<MeasuringAm,MeasuringAmsDB>();      
        }   
    }
}