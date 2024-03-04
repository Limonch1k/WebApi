using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfProtocol_to_GmfProtocolDB : Profile
    {
        public GmfProtocol_to_GmfProtocolDB() 
        {
            CreateMap<GmfProtocol, GmfProtocolDB>()
                .ForMember(prdb => prdb.KnNavigation, opt => opt.MapFrom(pr => new GmfPunct()
                {
                    GmfProtocols = null,
                    Bass = pr.KnNavigation.Bass,
                    Gmf = pr.KnNavigation.Gmf,
                    Gx = pr.KnNavigation.Gx,
                    Kod = pr.KnNavigation.Kod,
                    Lat = pr.KnNavigation.Lat,
                    Lon = pr.KnNavigation.Lon,
                    Pasp = pr.KnNavigation.Pasp,
                    Punkt = pr.KnNavigation.Punkt,
                    Reestr = pr.KnNavigation.Reestr,
                    Region = pr.KnNavigation.Region,
                    River = pr.KnNavigation.River,
                    Trans = pr.KnNavigation.Trans,
                }))
                .ForMember(prdb => prdb.PindicatorNavigation, opt => opt.MapFrom(pr => new GmfIndicator()
                {
                    CatNavigation = null,
                    GmfProtocols = null,
                    Cat = pr.PindicatorNavigation.Cat,
                    GroupIndic = pr.PindicatorNavigation.GroupIndic,
                    Iddop = pr.PindicatorNavigation.Iddop,
                    Idindicator = pr.PindicatorNavigation.Idindicator,
                    Idmain = pr.PindicatorNavigation.Idmain,
                    Mark = pr.PindicatorNavigation.Mark,
                    Namindicator = pr.PindicatorNavigation.Namindicator,
                }))
                .ReverseMap();
        }
    }
}
