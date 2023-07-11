using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;

namespace api_fact_weather_by_city.Mapper
{
    public class Synop_to_SynopDB : Profile
    {
        public Synop_to_SynopDB()
        {
            CreateMap<Synop,SynopDB>(); 
            
        }
        
    }
}