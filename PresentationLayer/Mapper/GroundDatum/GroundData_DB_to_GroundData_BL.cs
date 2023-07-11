using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;
using BL.Models;
using DatabaseLayer.DBModel;
using BusinessLayer.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class GroundData_DB_to_GroundData_BL : Profile
    {
        public GroundData_DB_to_GroundData_BL()
        {
            CreateMap<GroundData_DB, GroundData_BL>(); 
            
        }
        
    }
}