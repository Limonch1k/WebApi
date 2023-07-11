using System.Data;
using System;
using DB.TableModels;
using AutoMapper;
using DB.DBModels;
using BL.Models;
using PL.PLModels;

namespace api_fact_weather_by_city.Mapper
{
    public class SynopPL_to_SynopXML : Profile
    {
        public SynopPL_to_SynopXML()
        {
            CreateMap<SynopPL,SynopXML>()
            .ForMember(pl => pl.DateObservation, src => src.MapFrom(xml => xml.DateObs.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : xml.DateObs.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)))
            .ForMember(pl => pl.DateWrite, src => src.MapFrom(xml => xml.DateWrite.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture).Equals("0001-01-01:00-00-00") ? "NULL" : xml.DateWrite.ToString("yyyy-MM-dd:HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture)));
        } 
    }
}