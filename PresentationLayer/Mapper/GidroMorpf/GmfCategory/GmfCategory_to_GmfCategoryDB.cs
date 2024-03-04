using AutoMapper;
using DatabaseLayer.DBModel;
using DatabaseLayer.TableModels;

namespace api_fact_weather_by_city.Mapper
{
    public class GmfCategory_to_GmfCategoryDB : Profile
    {
        public GmfCategory_to_GmfCategoryDB()
        {
            CreateMap<GmfCategory, GmfCategoryDB>()
        
                .ForMember(catdb => catdb.GmfIndicators, opt => opt.MapFrom(cat => cat.GmfIndicators.Select(x => new GmfIndicator()
                {
                    Cat = x.Cat,
                    CatNavigation = null,
                    GmfProtocols = null,
                    GroupIndic = x.GroupIndic,
                    Iddop = x.Iddop,
                    Idindicator = x.Idindicator,
                    Idmain = x.Idmain,
                    Mark = x.Mark,
                    Namindicator = x.Namindicator

                })));
        }
    }
}
