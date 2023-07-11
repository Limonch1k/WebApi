using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using PL.PLModels;
using BusinessLayer.Models;
using api_fact_weather_by_city.ViewModel;

namespace api_fact_weather_by_city.Mapper
{
    public class GroundData_BL_to_GroundData_PL : Profile
    {
        public GroundData_BL_to_GroundData_PL()
        {
            CreateMap<GroundData_BL, GroundData_PL>(); 
            
        }
        
    }
}