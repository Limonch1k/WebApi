
using BL.Models;

namespace BL.IServices
{
    public interface IUserServices<User> where User : class
    {
        public Task Create(string userId, string ApiKey);

        public Task Delete(User item);
        
        public  Task<User> FindById(int id);

        public Task<IEnumerable<User>> GetAll();

        public int GetUserId(string userId);

        public string[] GetAvailablePage(string userId);

        public string[] GetAvailableParam(int userId);

        public string[] GetAvailablePunkt(int userId);
        public Task Save();

        public Task Update(int id, User item);

        public void Dispose();

    }
}
