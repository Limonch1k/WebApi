using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;
using BL.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class SynopDB_to_SynopBL : Profile
    {
        public SynopDB_to_SynopBL()
        {
            CreateMap<SynopDB,SynopBL>(); 
            
        }
        
    }
}