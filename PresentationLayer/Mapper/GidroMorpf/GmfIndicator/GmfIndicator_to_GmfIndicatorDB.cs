using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfIndicator_to_GmfIndicatorDB : Profile
    {
        public GmfIndicator_to_GmfIndicatorDB() 
        {
            CreateMap<GmfIndicator, GmfIndicatorDB>()
                .ForMember(inddb => inddb.GmfProtocols, opt => opt.MapFrom(ind => ind.GmfProtocols.Select(x => new GmfProtocol() 
                {
                    Aestimation = x.Aestimation,
                    Bestamination = x.Bestamination,
                    God = x.God,
                    Kn = x.Kn,
                    KnNavigation = null,
                    MarkEstam = x.MarkEstam,
                    Pcat = x.Pcat,
                    Pindicator = x.Pindicator,
                    PindicatorNavigation = null,
                    Prim = x.Prim,
                    Оpisanie = x.Оpisanie
                }) 
            ))
                .ForMember(inddb => inddb.CatNavigation, opt => opt.MapFrom(ind => new GmfCategory() 
                {
                    GmfIndicators = null,
                    Idcat = ind.CatNavigation.Idcat,
                    Namecat = ind.CatNavigation.Namecat,
                    Nzona = ind.CatNavigation.Idcat,
                    NzonaNavigation = ind.CatNavigation.NzonaNavigation
                }));
        }
    }
}
