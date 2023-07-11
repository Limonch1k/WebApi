using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;
using BL.Models;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class SynopBL_to_SynopPL : Profile
    {
        public SynopBL_to_SynopPL()
        {
            CreateMap<SynopBL,SynopPL>(); 
            
        }
        
    }
}