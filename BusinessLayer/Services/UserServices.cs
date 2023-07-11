using BL.IServices;
using DBLayer.Context;
using BL.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BL.BLModels;
using Static.Service;
using Microsoft.Extensions.Logging;
using DB.TableModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BL.Services
{
    public class UserServices<T> : IUserServices<T> where T : UserBL
    {
        private UserdbContext _user{get;set;}

        private IMapper _mapper {get;set;}

        private ILogger _logger { get; set; }
        
        public UserServices(UserdbContext user, IMapper mapper, ILoggerProvider loggerProvider)
        {
            _user = user;
            _mapper = mapper;
            _logger = loggerProvider.CreateLogger("UserServiceLogger");
        }

        public Task Create(string userId, string ApiKey)
        {
            try
            {
                _user.Users.Where(u => u.UserId.Equals(userId) && u.ApiKey.Equals(ApiKey)).Any();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at CheckPassword");
            }
            finally
            {
                throw new Exception("an errore has occured");
            }
        }

        public Task Delete(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, T item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _user.DisposeAsync();
        }

        public bool CheckPassword(string ApiKey)
        {
            bool ret = false;
            try
            {
                ret = _user.Users.Where(u => u.ApiKey.Equals(ApiKey)).Any();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at CheckPassword");
            }
            finally 
            {
                
            }
            return ret;
        }

        public bool CheckAccessToAnyPage(string userId, string ApiKey)
        {
            
            try
            {
                var ret = _user.Users.Where(u => u.UserId.Equals(userId) && u.ApiKey.Equals(ApiKey))
                .Include(u => u.right_list).Where(u => u.right_list.Any()).Any();
                return ret;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at CheckAccessToAnyPage");
            }
            finally
            {
                throw new Exception("an errore has occured");
            }
        }

        public UserBL FindUserByNameAndPassword(string userId, string ApiKey)
        {
            try
            {
                var user = _user.Users.Where(u => u.UserId.Equals(userId) && u.ApiKey.Equals(ApiKey));
                return _mapper.Map<UserBL>(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at FindUserByNameAndPassword");
            }
            finally
            {
                throw new Exception("an errore has occured");
            }
        }

        public string[] GetAvailablePage(string userId)
        {
            string[]? page = null;
            try
            {
                var user = _user.Users.Where(u => u.UserId == Int32.Parse(userId)).Include(u => u.right_list).FirstOrDefault();
                page = user.right_list.OrderByDescending(c => c.Id).Select(c => c.Source).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetAvailablePage");
            }
            finally
            {
                
            }
            return page;
        }

        public string[] GetAvailablePunkt(int userId)
        {
            try
            {
                return _user.Users.Where(u => u.UserId == userId).Include(u => u.punkt_list).SelectMany(c => c.punkt_list).Select(punkt => punkt.Punkt_Id.ToString()).AsQueryable().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetAvailablePunkt");
            }
            finally
            {
                throw new Exception("an errore has occured");
            }
        }

        public string[] GetAvailableParam(int userId)
        {
            try
            {
                var a = _user.Users.Where(u => u.UserId == userId).ToList();
                return _user.Users.Where(u => u.UserId == userId).Include(u => u.punkt_list).SelectMany(c => c.param_list).Select(punkt => punkt.Param_Name.ToString()).AsQueryable().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetAvailableParam");
            }
            finally
            {
                throw new Exception("an errore has occured");
            }
        }

        public int GetUserId(string api_key)
        {
            int id = -1;
            try
            {
                id = _user.Users.Where(u => u.ApiKey.Equals(api_key)).Select(u => u.UserId).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetUserId");
            }
            finally
            {
                
            }
            return id;
        }
    }
}