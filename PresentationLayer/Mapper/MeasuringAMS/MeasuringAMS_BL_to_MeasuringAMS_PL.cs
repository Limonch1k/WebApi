using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class MeasuringAMS_BL_to_MeasuringAMS_PL : Profile
    {
        public MeasuringAMS_BL_to_MeasuringAMS_PL()
        {
            CreateMap<MeasuringAMS_BL,MeasuringAMS_PL>(); 
            
        }
        
    }
}