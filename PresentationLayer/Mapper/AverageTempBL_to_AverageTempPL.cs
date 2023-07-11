using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class AverageTempBL_to_AverageTempPL : Profile
    {
        public AverageTempBL_to_AverageTempPL()
        {
            CreateMap<AverageTempBL,AverageTempPL>();         
        }
        
    }
}