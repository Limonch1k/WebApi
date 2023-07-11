using AutoMapper;
using DatabaseLayer.IDbContext;
using DBLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.TableModels;
using DatabaseLayer.DBModel;

namespace DatabaseLayer.Repository
{
    public class UniversalUserRepository<TableModel> where TableModel : class 
    {
        private IMapper _mapper { get; set; }

        private UserdbContext _userContext { get; set; }

        private ILogger _logger { get; set; }

        public UniversalUserRepository(UserdbContext userdbContext, IMapper mapper, ILoggerProvider loggerProvider) 
        {
            _mapper = mapper;
            _userContext = userdbContext;
            _logger = loggerProvider.CreateLogger("UserRepoLogger");
        }

        public void CreateUser(string userId = null, string api_key = null) 
        {
            if (api_key is null && userId is null)
            {
                _userContext.Users.Add(new User() { ApiKey = "-1" });
            }
            else if (userId is not null && api_key is null)
            {
                _userContext.Users.Add(new User() { ApiKey = "-1", UserId = Int32.Parse(userId) });
            }
            else if (userId is null && api_key is not null)
            {
                _userContext.Users.Add(new User() { ApiKey = api_key });
            }
            else 
            {
                _userContext.Users.Add(new User() { ApiKey = api_key, UserId = Int32.Parse(userId) });
            }

            return;
        }

        public UserDB GetUser(string user_id) 
        {
            var user = _userContext.Users.Where(u => u.UserId == Int32.Parse(user_id)).AsQueryable().FirstOrDefault();

            var userdb = _mapper.Map<UserDB>(user);

            return userdb;
        }

        public void UpdateUser(string user_Id, string api_key) 
        {

        }

        public void DeleteUser(string user_id) 
        {
            var us = _userContext.Users.Where(u => u.UserId.Equals(user_id)).FirstOrDefault();
            _userContext.Users.Remove(us);
        }

        public void CreateApiKey(string user_id, string api_key) 
        {
            var us = _userContext.Users.Where(u => u.UserId.Equals(user_id)).FirstOrDefault();
            us.ApiKey = api_key;
        }

        public string GetApiKey(string user_id) 
        {
            var us = _userContext.Users.Where(u => u.UserId.Equals(user_id)).FirstOrDefault();
            return us.ApiKey;
        }

        public void UpdateApiKey(string user_id, string apiKey) 
        {
            var us = _userContext.Users.Where(u => u.UserId.Equals(user_id)).FirstOrDefault();
            us.ApiKey = apiKey;
        }

        public void DeleteApiKey(string user_id) 
        {
            var us = _userContext.Users.Where(u => u.UserId.Equals(user_id)).FirstOrDefault();
            us.ApiKey = "-1";
        }

        public async Task SaveChangesAsync() 
        {
            _userContext.SaveChangesAsync();
            return;
        }

        public async Task Dispose() 
        {
            _userContext.Dispose();
        }

        private DbContext GetContext<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
        {
            var dbcontext = dbSet.GetService<ICurrentDbContext>().Context;
            return dbcontext;
        }

    }
}
