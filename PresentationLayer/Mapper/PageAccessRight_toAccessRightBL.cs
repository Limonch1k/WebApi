using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using BL.BLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class PageAccessRight_to_AccessRightBL : Profile
    {
        public PageAccessRight_to_AccessRightBL()
        {
            CreateMap<PageAccessRight,AccessRightBL>().ReverseMap();         
        }
        
    }
}