using DatabaseLayer.ParamModel;

namespace DB.IRepository
{
    public interface IResorceRepositoryAsync<T> where T : class
    {
        public Task<T> GetByIdAsync();

        public Task<List<T>> GetAllAsync();
        //

        public Task<List<T>> ResourceIdParamStartDateEndDateOrderbyFilter(MeteoParamModel_DL paramModel);

        //

        public Task<List<T>> GetLastOrderbyFilter(MeteoParamModel_DL paramModel);

        public Task<List<T>> GetLastResourceIdOrderbyFilter(MeteoParamModel_DL paramModel); 

        public Task<List<T>> GetLastParamOrderbyFilter(MeteoParamModel_DL paramModel); 

        public Task<List<T>> GetLastResourceIdParamOrderbyFilter(MeteoParamModel_DL paramModel);

        //

        public Task SaveAsync();

        public void DisposeAsync();

    }
}