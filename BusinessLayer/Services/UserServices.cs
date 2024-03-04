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
using GeneralObject.StringCryptography;

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

        public Task Create(string ApiKey)
        {
            try
            {
                if(!_user.Users.Where(u => u.ApiKey.Equals(ApiKey)).Any())
                {
                    _user.Users.Add( new User() { ApiKey = ApiKey});
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at CheckPassword");
            }

            return null;
        }

        public Task Delete(T item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByKey(string ApiKey)
        {
            try
            {
                var user = _user.Users.Where(u => u.ApiKey.Equals(ApiKey)).FirstOrDefault();
                if(user is not null)
                {
                    _user.Users.Remove(user);
                }
                else
                {

                }
            }
            catch( Exception e)
            {

            }

            return null;
        }

        public Task<T> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var user_bl = _user.Users.Include(x => x.right_list).ToList();
            List<UserBL> list = new List<UserBL>();

            foreach(var bl in user_bl)
            {
                UserBL user = new UserBL();
                var decrypt = bl.ApiKey.DecryptString();
                var mass = decrypt.Split(' ');
                user.Password  = mass[1];
                user.Username = mass[0];
                user.right = bl.right_list;
                list.Add(user);
            }

            return list as IEnumerable<T>;
        }

        public async Task Save()
        {
            _user.SaveChanges();
            return;
        }

        public async Task Update(int id, T item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _user.DisposeAsync();
        }

        public bool CheckUserPassword(string EncryptKey)
        {
            bool ret = false;

            try
            {
                ret = _user.Users.Where(u => u.ApiKey.Equals(EncryptKey.EncryptString())).Any();
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

        public string GetPasswordByUserName(string name)
        {
            var str = "";

            var list = _user.Users.ToList();
            foreach(var l in list)
            {
                try
                {
                    str = l.ApiKey.DecryptString();
                    if (str.Contains(name + " "))
                    {
                        break;
                    }
                }
                catch(Exception e)
                {

                }

            }
            
            return str;
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

        public UserBL FindUserByNameAndPassword(string ApiKey)
        {
            try
            {
                var user = _user.Users.Where(u => u.ApiKey.Equals(ApiKey));
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
            string[] str = null;
            try
            {
                str = _user.Users.Where(u => u.UserId == userId).Include(u => u.punkt_list).SelectMany(c => c.punkt_list).Select(punkt => punkt.Punkt_Id.ToString()).AsQueryable().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetAvailablePunkt");
            }
            finally
            {
               
            }

            return str;
        }

        public string[] GetAvailableParam(int userId)
        {
            string[] str = null;
            try
            {
                var a = _user.Users.Where(u => u.UserId == userId).ToList();
                str = _user.Users.Where(u => u.UserId == userId).Include(u => u.punkt_list).SelectMany(c => c.param_list).Select(punkt => punkt.Param_Name.ToString()).Distinct().AsQueryable().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in UserService at GetAvailableParam");
            }
            finally
            {
            
            }
            return str;
        }

        public int GetUserId(string api_key)
        {
            int id = -1;
            try
            {
                id = _user.Users.Where(u => u.ApiKey.Equals(api_key)).Select(x => x.UserId).FirstOrDefault();
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

        public int GetUserIdByKey(string api_key)
        {
            int id = -1;
            try
            {
                id = _user.Users.Where(u => u.ApiKey.Equals(api_key)).Select(x => x.UserId).FirstOrDefault();
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


        public bool SetUserRole(int user_id, string role)
        {
            bool b = true;
            try
            {
                var user = _user.Users.Where(u => u.UserId == user_id).FirstOrDefault();
                
                PageAccessRight p = new PageAccessRight() { Source = role, UserId = user_id};

                _user.AccessRights.Add(p);
            }
            catch(Exception e)
            {
                b = false;
            }

            return b;
        }

        public bool DeleteUserRole(int userId, string role)
        {
            bool b = true;
            try
            {
                var user = _user.Users.Where(x => x.UserId == userId).Include(x => x.right_list).FirstOrDefault();
                var user_right = user.right_list.Where(x => x.Source.Equals(role)).FirstOrDefault();

                _user.AccessRights.Remove(user_right);
            }
            catch(Exception e)
            {
                b = false;
            }

            return b;
        }


        public List<UserBL> GetUsersByRole(string[] role)
        {
            var users = _user.Users.Where(u => u.right_list.Where(x => role.Contains(x.Source)).Any())
            .Include(x => x.right_list.Where(r => role.Contains(r.Source))).ToList();//.Select(x => x.ApiKey).ToArray();

            List<UserBL> users_list = new List<UserBL>(); 

            foreach(var k in users)
            {
                try
                {
                    var p = k.ApiKey.DecryptString();

                    var name = p.Split(' ')[0];

                    var us = new UserBL();

                    us.Username = name;

                    us.right = k.right_list;

                    users_list.Add(us);
                }
                catch(Exception e)
                {

                }
                
            }

            return users_list;
        }

        public int GetUserIdByName(string username)
        {
            var list = _user.Users.ToList();

            User user = null;

            foreach(var l in list)
            {
                try
                {
                    var decrypt = l.ApiKey.DecryptString();

                    if(decrypt.Split(' ')[0].Equals(username))
                    {
                        user = l;
                        break;        
                    }
                }
                catch(Exception e)
                {

                }
                
            }

            if (user is null)
            {
                return 0;
            }

            return user.UserId;
        }

        public bool AddUser(string username, string password)
        {
            var i = this.GetUserIdByName(username);

            if (i != 0)
            {
                return false;
            }

            var api_key = (username + " " + password).EncryptString();

            User user = new User();

            user.ApiKey = api_key;

            _user.Users.Add(user);

            _user.SaveChanges();

            return true;
        }

        public bool RemoveUser(string username)
        {
            var i = this.GetUserIdByName(username);

            if (i == 0)
            {
                return false;
            }

            var user = _user.Users.Where(x => x.UserId == i).FirstOrDefault();

            _user.Users.Remove(user);

            _user.SaveChanges();

            return true;
        }
    }
}