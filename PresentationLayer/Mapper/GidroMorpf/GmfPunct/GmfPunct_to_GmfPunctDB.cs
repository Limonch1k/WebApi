using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper;
public class GmfPunct_to_GmfPunctDB : Profile
{
    public GmfPunct_to_GmfPunctDB() 
    {
        CreateMap<GmfPunct, GmfPunctDB>()
            .ForMember(puncdb => puncdb.GmfProtocols, opt => opt.MapFrom(punc => punc.GmfProtocols.Select(x => new GmfProtocol() 
            {
                Aestimation = x.Aestimation,
                Bestimation = x.Bestimation,
                God = x.God,
                Kn = x.Kn,
                KnNavigation = null,
                MarkEstam = x.MarkEstam,
                Pcat = x.Pcat,
                Pindicator = x.Pindicator,
                PindicatorNavigation = null,
                Prim = x.Prim,
                Opisanie = x.Opisanie
            }))).ReverseMap();
    }
}

