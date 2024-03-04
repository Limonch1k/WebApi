
using BL.Models;

namespace BL.IServices
{
    public interface IUserServices<UserBL> where UserBL : class
    {
        public Task Create(string ApiKey);

        public Task Delete(UserBL item);
        
        public  Task<UserBL> FindById(int id);

        public Task<IEnumerable<UserBL>> GetAll();

        public int GetUserId(string userId);
        public int GetUserIdByKey(string userId);

        public string GetPasswordByUserName(string password);

        public string[] GetAvailablePage(string userId);

        public string[] GetAvailableParam(int userId);

        public string[] GetAvailablePunkt(int userId);
        public Task Save();

        public Task Update(int id, UserBL item);

        public void Dispose();

    }
}
