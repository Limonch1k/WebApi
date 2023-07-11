using BL.Services;

namespace BusinessLayer.IServices
{
    public interface INoDataServices<Data,Model> where Data : class where Model : class 
    {
        public List<Model> CheckNullDataInList(List<Data> station_list, string[] param_list);

        public List<Model> CheckCityModelingNullData(List<Data> city_list, string[] param_list);

        public void PutErrorData(int UserId, List<Model> error_list);

        public void CalculateDiscont(int UserId, List<Model> model);

        public Task Save();

        public Task Dispose();
    }
}