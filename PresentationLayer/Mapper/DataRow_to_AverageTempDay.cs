using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;

namespace api_fact_weather_by_city.Mapper
{
    public class DataRow_to_AverageTempDB : Profile
    {
        public DataRow_to_AverageTempDB()
        {
            CreateMap<DataRow,AverageTempDB>()
            .ForMember(d => d.averageTemp, con => con.MapFrom(((src, d, dmember, context) => !src.IsNull((string)context.Items["temp"]) ? Math.Round((double)src.Field<System.Double>((string)context.Items["temp"]), 2) : (double?)null)))  
            .ForMember(d => d.stationId, con => con.MapFrom(((src, d, dmember, context) => src.Field<string>((string)context.Items["station_id"]))))         
            .ForMember(d => d.date, con => con.MapFrom(((src, d, dmember, context) => src.Field<DateTime>((string)context.Items["date"]).ToString("yyyy-MM-dd"))))           
            .ForMember(d => d.city, con => con.MapFrom((src, d, dmember, context) => src.Field<string>((string)context.Items["locality"])));
            
        }
        
    }
}