using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using BL.Models;
using DB.DBModels;

namespace api_fact_weather_by_city.Mapper
{
    public class AverageTempDB_to_AverageTempBL : Profile
    {
        public AverageTempDB_to_AverageTempBL()
        {
            CreateMap<AverageTempDB,AverageTempBL>()
            .ForMember(db => db.CityName, con => con.MapFrom(bl => bl.city))
            .ForMember(db => db.CityId, con => con.MapFrom(bl => bl.stationId));        
        }
        
    }
}