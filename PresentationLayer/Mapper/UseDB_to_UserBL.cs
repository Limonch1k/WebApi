using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;

namespace api_fact_weather_by_city.Mapper
{
    public class User_to_UserBL : Profile
    {
        public User_to_UserBL()
        {
            CreateMap<User,UserBL>();         
        }
        
    }
}