using AutoMapper;
using BusinessLayer.ParametrModel;
using DatabaseLayer.ParamModel;
using DB.DBModels;
using DB.TableModels;

namespace api_fact_weather_by_city.Mapper.ParamModels
{
        public class MeteoParamModel_BL_to_MeteoParamModel_DB : Profile
        {
            public MeteoParamModel_BL_to_MeteoParamModel_DB()
            {
                CreateMap<MeteoParamModel_BL, MeteoParamModel_DL>();

            }

        }
}
