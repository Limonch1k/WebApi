using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;
using BL.Models;
using DatabaseLayer.DBModel;

namespace api_fact_weather_by_city.Mapper
{
    public class MeasuringAMS_DB_to_MeasuringAMS_BL : Profile
    {
        public MeasuringAMS_DB_to_MeasuringAMS_BL()
        {
            CreateMap<MeasuringAmsDB,MeasuringAMS_BL>(); 
            
        }
        
    }
}