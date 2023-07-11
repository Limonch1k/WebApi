using AutoMapper;
using BL.Models;
using BL.IServices;
using DBLayer.Context;
using DB.TableModels;
using DB.DBModels;
using BusinessLayer.ParametrModel;

namespace BL.Services
{
    public class TempServices<T> : ISynopServicesAsync<T> where T : AverageTempBL
    {

        private CligtsContext _cligts {get;set;}

        private IMapper _mapper {get;set;}

        public TempServices(IMapper mapper, CligtsContext cligts)
        {
            _cligts = cligts;
            _mapper = mapper;
        }

        public Task<List<T>> SelectAll(string[] orderby = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> SelectStationParamSpecifyDays(MeteoParamModel_BL paramObject)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GenericCallHandlerOfParamObject(MeteoParamModel_BL paramObject)
        {
            throw new NotImplementedException();
        }
    }
}