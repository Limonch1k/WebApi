using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class UseDB_to_UserBL : Profile
    {
        public UseDB_to_UserBL()
        {
            CreateMap<AverageTempBL,AverageTempPL>();         
        }
        
    }
}