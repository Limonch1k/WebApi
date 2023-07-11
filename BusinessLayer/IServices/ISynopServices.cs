using BusinessLayer.ParametrModel;

namespace BL.IServices
{
    public interface ISynopServicesAsync<T> where T : class
    {
        ///////////GetDataByFILTERS///////////
        public Task<List<T>> SelectAll(string[] orderby = null);

        /////////////////////////////////////

        /////////////////////////////////////
        
        public Task<List<T>> SelectStationParamSpecifyDays(MeteoParamModel_BL paramObject);
        public Task Save();

        public Task Dispose();

        public Task<List<T>> GenericCallHandlerOfParamObject(MeteoParamModel_BL paramObject);
    }
}